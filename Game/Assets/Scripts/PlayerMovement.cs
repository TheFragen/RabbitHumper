using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    private string playerString;
    public float acceleration = 10;
    public float jumpSpeed = 10;
    public float maxVelocity;
    float distToGround;
    public bool isJumping = false;
    Rigidbody rb;
    public Animator anim;
    Vector3 moveDir = Vector3.zero;
    bool isStandingOnBullet = false;
    XboxController controller;

    // Use this for initialization
    void Start()
    {
        switch (playerNumber)
        {
            case 1:
                controller = XboxController.First;
                break;
            case 2:
                controller = XboxController.Second;
                break;
        }
        playerString = playerNumber.ToString();

        rb = this.GetComponent<Rigidbody>();

        distToGround = this.GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isStandingOnBullet = false;
    }

    void FixedUpdate()
    {
        
        //Accelereation
        if (XCI.GetAxis(XboxAxis.LeftStickX, controller) != 0)
        {
            moveDir = new Vector3(0, 0, XCI.GetAxis(XboxAxis.LeftStickX, controller));
            if (Mathf.Abs(rb.velocity.z) <= maxVelocity)
            {
                anim.SetBool("isRunning", true);
                anim.SetFloat("runSpeed", 1f);
                rb.AddForce(moveDir * acceleration);

                if (rb.velocity.normalized.z != 0)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, rb.velocity.normalized.z));
                }
            }
            //Deacceleration
        }
        else if (!isJumping)
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("runSpeed", 1f);
            rb.velocity = rb.velocity / 1.2f;
        }

        //No animation if no movement
        if (rb.velocity.sqrMagnitude < 0.2f)
        {
            anim.Play("Standing");
            anim.SetBool("isRunning", false);
        }

        //Less running movement if jumping
        if (isJumping)
        {
            anim.SetFloat("runSpeed", 0.2f);
        }

        //No running if falling straight down
        if (Mathf.Abs(rb.velocity.z) < 0.1f)
        {
            anim.SetBool("isRunning", false);
        }

        //Jumping
        if (XCI.GetButton(XboxButton.A, controller) && !isStandingOnBullet && !isJumping)
        {
            //Fix velocity so player can't mega jump on carrot
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            anim.SetBool("isRunning", true);
            rb.AddForce(new Vector3(0, 100, 0) * jumpSpeed);
        }
    }

    public int getPlayerNumb()
    {
        return playerNumber;
    }
    public void setPlayerNumb(int i)
    {
        playerNumber = i;
    }

    public void setPlayerColor(Material[] mats)
    {
        Debug.Log(transform.GetChild(0).Find("polySurface108").GetComponent<Renderer>().material);
        Material[] _tmpA = this.transform.GetChild(0).Find("polySurface108").GetComponent<Renderer>().materials;
        _tmpA[2] = mats[0];
        _tmpA[3] = mats[1];
        this.transform.GetChild(0).Find("polySurface108").GetComponent<Renderer>().materials = _tmpA;

        Material[] _tmpB = this.transform.GetChild(0).Find("polySurface117").GetComponent<Renderer>().materials;
        _tmpB[1] = mats[1];
        this.transform.GetChild(0).Find("polySurface117").GetComponent<Renderer>().materials = _tmpB;

        Material[] _tmpC = this.transform.GetChild(0).Find("polySurface119").GetComponent<Renderer>().materials;
        _tmpC[1] = mats[1];
        this.transform.GetChild(0).Find("polySurface119").GetComponent<Renderer>().materials = _tmpC;
    }

}
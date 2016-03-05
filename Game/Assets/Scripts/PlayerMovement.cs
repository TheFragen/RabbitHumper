using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public float acceleration = 10;
    public float jumpSpeed = 10;
    public float maxVelocity;
    float distToGround;
    public bool isJumping = false;
    Rigidbody rb;
    public Animator anim;
    Vector3 moveDir = Vector3.zero;
    bool isStandingOnBullet = false;

    // Use this for initialization
    void Start () {
        
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
        if (Input.GetAxis("Horizontal") != 0)
        {
            moveDir = new Vector3(0, 0, Input.GetAxis("Horizontal"));
            if (Mathf.Abs(rb.velocity.z) <= maxVelocity)
            {
                this.GetComponent<PlayerAudioHandler>().playRunning();
                anim.SetBool("isRunning", true);
                anim.SetFloat("runSpeed", 1f);
                rb.AddForce(moveDir * acceleration);

                if (rb.velocity.normalized.z != 0)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, rb.velocity.normalized.z));
                }
            }
        //Deacceleration
        } else if (!isJumping)
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("runSpeed", 1f);
            rb.velocity = rb.velocity / 1.2f;
        }

        //No animation if no movement
        if(rb.velocity.sqrMagnitude < 0.2f)
        {
            anim.Play("Standing");
            anim.SetBool("isRunning", false);
            this.GetComponent<PlayerAudioHandler>().playStopping();
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
            this.GetComponent<AudioSource>().Stop();
        }

        //Jumping
        if (Input.GetButton("Fire2") && !isStandingOnBullet && !isJumping)
        {
            //Fix velocity so player can't mega jump on carrot
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            anim.SetBool("isRunning", true);
            rb.AddForce(new Vector3(0, 100, 0) * jumpSpeed);
        }
    }
}

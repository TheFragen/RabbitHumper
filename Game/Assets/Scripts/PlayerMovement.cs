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
    Animator anim;
    Vector3 moveDir = Vector3.zero;
    bool isStandingOnBullet = false;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
        
        rb = this.GetComponent<Rigidbody>();
        
        distToGround = this.GetComponent<Collider>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        
        isJumping = !isGrounded();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    void OnTriggerExit()
    {
        isStandingOnBullet = false;
    }
    
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            moveDir = new Vector3(0, 0, Input.GetAxis("Horizontal"));
            if (Mathf.Abs(rb.velocity.z) <= maxVelocity)
            {
                anim.SetBool("isRunning", true);
                rb.AddForce(moveDir * acceleration);
            }
        } else if (!isJumping)
        {
            anim.SetBool("isRunning", false);
            rb.velocity = rb.velocity / 1.2f;
        } else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetButton("Fire2") && !isJumping && !isStandingOnBullet)
        {
            //Fix velocity so player can't mega jump on carrot
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, 100, 0) * jumpSpeed);
            
        }
    }

    public bool isGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}

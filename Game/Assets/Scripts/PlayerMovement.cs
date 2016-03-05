using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
	public int playerNumber = 1;
	private string playerString;
    public float acceleration = 10;
    public float jumpSpeed = 10;
    public float maxVelocity;
    float distToGround;
    public bool isJumping = false;
    Rigidbody rb;
    Vector3 moveDir = Vector3.zero;

	// Use this for initialization
	void Start () {
		playerNumber = 1;
		playerString = playerNumber.ToString ();
        rb = this.GetComponent<Rigidbody>();

        distToGround = this.GetComponent<Collider>().bounds.extents.y;
    }
	
	// Update is called once per frame
	void Update () {
        isJumping = !isGrounded();
    }
    
    void FixedUpdate()
    {
		if (Input.GetAxis("LeftJoystickX"+playerString) != 0)
        {
			Debug.Log ("LeftJoystickX" + playerString);
			moveDir = new Vector3(0, 0, Input.GetAxis("LeftJoystickX"+playerString));
			if (Mathf.Abs(rb.velocity.z) <= maxVelocity)
            {
				rb.AddForce(moveDir * acceleration);
            }
        } else if (!isJumping)
        {
            rb.velocity = rb.velocity / 1.2f;
        }

		if (Input.GetButton("X"+playerString) && !isJumping)
        {
            rb.AddForce(new Vector3(0, 100, 0) * jumpSpeed);
        }
    }

    public bool isGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

	public void setPlayerNumb(int i){
		playerNumber = i;
	}
	public int getPlayerNumb(){
		return playerNumber;
	}
}

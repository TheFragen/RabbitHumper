using UnityEngine;
using System.Collections;

public class Kid : MonoBehaviour {
	private Vector3 jumpDir;
	public float maxJumpForce = 1.0f;
	public float minJumpForce = 1.0f;
	public float jumpTimer = 1.5f;

	private float startTime;
	private float distToGround;


	void Start(){
		distToGround = this.GetComponent<Collider>().bounds.extents.y;

		jump ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

		if (isGrounded ()) {
			if (Time.time >= startTime + jumpTimer) {
				jump ();
			}	
		}
	}

	void jump(){
		jumpDir = Random.insideUnitSphere;
		if (jumpDir.y < 0.0f) {
			jumpDir.y *= -1;
		}
		float jumpForce = Random.Range (minJumpForce, maxJumpForce);
		GetComponent<Rigidbody> ().AddForce (jumpDir * jumpForce);
		startTime = Time.time;
	}

	public void setColor(Color matColor){
		GetComponent<MeshRenderer>().material.color =  matColor;
	}

	bool isGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

}

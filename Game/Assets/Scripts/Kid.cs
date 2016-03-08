using UnityEngine;
using System.Collections;

public class Kid : MonoBehaviour {
	private Vector3 jumpDir;
	public float maxJumpForce = 100.0f;
	public float minJumpForce = 200.0f;
	public float jumpTimer = 1.0f;

	private float startTime;
	private float distToGround;
    public AudioClip[] bounceSounds = new AudioClip[5];

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
        if(Random.Range(0,10) >= 8)
        {
            this.GetComponent<AudioSource>().PlayOneShot(bounceSounds[Random.Range(0, bounceSounds.Length)]);
        }
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarrotMovement : MonoBehaviour {
    public float smallAngle = 10;
	public float knockback = 50.0f;
    bool isFired = false;
    Transform spawnPoint;
    Transform parentTransform;
    public AudioClip carrotAtPlayer;
    public AudioClip carrotAtWall;
    public AudioClip[] carrotAtCarrot = new AudioClip[2];

    // Use this for initialization
    void Start () {
        parentTransform = this.transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
        if (parentTransform.position.x > 0.08f || parentTransform.position.x < 0.08f)
        {
            var _tmp = parentTransform.position;
            _tmp.x = 0.08f;
            parentTransform.position = _tmp;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Platform")
        {
            if(parentTransform != null)
            {
                GetComponent<AudioSource>().PlayOneShot(carrotAtWall);
                parentTransform.GetComponent<Rigidbody>().useGravity = false;
                parentTransform.GetComponent<Rigidbody>().isKinematic = true;
                parentTransform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                isFired = false;
            }
        }
        if(other.tag == "Bullet")
        {
            Rigidbody otherRB = other.transform.parent.GetComponent<Rigidbody>();
            Rigidbody thisRB = parentTransform.GetComponent<Rigidbody>();
            
            if (otherRB.velocity.magnitude != 0 && isFired)
            {
                float dotProduct = Vector3.Dot(thisRB.velocity, otherRB.velocity);
                if(dotProduct <= 0)
                {
                    Vector3 _curPosThis = parentTransform.position;
                    Vector3 _curPosOther = other.transform.parent.position;
                    thisRB.velocity = new Vector3(0, 5, -thisRB.velocity.z / 1.3f);                   
                    otherRB.velocity = new Vector3(0, 5, -otherRB.velocity.z / 1.3f);
                    GetComponent<AudioSource>().PlayOneShot(carrotAtCarrot[Random.Range(0,1)]);

                    //  Debug.Log("Towards");
                }
            }
        }
		if (other.tag == "Player1" || other.tag == "Player2") {
            GetComponent<AudioSource>().PlayOneShot(carrotAtPlayer);
			Rigidbody otherRB = other.transform.GetComponent<Rigidbody>();
			Rigidbody thisRB = parentTransform.GetComponent<Rigidbody>();

			Vector3 dir = thisRB.velocity.normalized;
			Vector3 knockbackDir = Vector3.zero;

			if (other.GetComponent<PlayerMovement> ().isJumping) {
				if (dir.y > 0.0f) {
					knockbackDir += Vector3.up;
				}
				if (dir.y < 0.0f) {
					knockbackDir += Vector3.down;
				}
			}
			if (dir.z > 0.0f) {
				knockbackDir += Vector3.forward;
			}
			if (dir.z < 0.0f) {
				knockbackDir += Vector3.back;
			}

			otherRB.AddForce (knockbackDir * knockback);

		}
        
    }

    void FixedUpdate()
    {
        if (isFired)
        {
            if(parentTransform.GetComponent<Rigidbody>().velocity.magnitude > 0)
            {
                parentTransform.GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(parentTransform.GetComponent<Rigidbody>().velocity));
            }
        }
    }

    public void fireProjectile()
    {
        isFired = true;
        this.spawnPoint = this.transform;
    }
}

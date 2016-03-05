using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarrotMovement : MonoBehaviour {
    public float smallAngle = 10;
    bool isFired = false;
    Transform spawnPoint;
    Transform parentTransform;
    public List<Vector3> thisVelocity;

    // Use this for initialization
    void Start () {
        parentTransform = this.transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
        if(parentTransform.GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            thisVelocity.Add(parentTransform.GetComponent<Rigidbody>().velocity);
        }

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
                parentTransform.GetComponent<Rigidbody>().useGravity = false;
                parentTransform.GetComponent<Rigidbody>().isKinematic = true;
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
                    

                    //  Debug.Log("Towards");
                }
            }
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

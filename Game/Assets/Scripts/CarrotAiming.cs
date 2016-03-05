using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarrotAiming : MonoBehaviour {
    public Transform carrot;
    public Transform spawnedCarrot;
    Rigidbody carrotRB;
    Vector3 lastDirection = Vector3.zero;
    public float projectileForce;
    float lastFireTime = 0;
    public float fireWaitTime = 0;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time * 1000;

        Vector3 direction = Vector3.Normalize(new Vector3(0, -Input.GetAxis("Right_thumbY"), Input.GetAxis("Right_thumbX")));

        //Show carrot when player holds down R2
        if (Input.GetButtonDown("R2") && spawnedCarrot == null && currentTime > lastFireTime){
            spawnedCarrot = Instantiate(carrot, this.transform.position,Quaternion.Euler(new Vector3(10,0,0))) as Transform;
            carrotRB = spawnedCarrot.GetComponent<Rigidbody>();
        }
        
        //"Animate" when the player rotates the right thumb stick
        if(spawnedCarrot != null && Input.GetButton("R2"))
        {
            carrotRB.useGravity = false;
            if (direction.magnitude == 0)
            {
                spawnedCarrot.position = this.transform.position;
                if(lastDirection.magnitude > 0)
                {
                    carrotRB.MoveRotation(Quaternion.LookRotation(lastDirection));
                } else
                {
                    carrotRB.MoveRotation(Quaternion.Euler(0,0,0));
                }

            } else
            {
                spawnedCarrot.position = this.transform.position;
                carrotRB.MoveRotation(Quaternion.LookRotation(direction));
                lastDirection = direction;
            }
        }

        //Fire the carrot
        if (Input.GetButtonUp("R2") && spawnedCarrot != null)
        {
            Vector3 fireDirection = Vector3.Normalize(new Vector3(0, -Input.GetAxis("Right_thumbY"), Input.GetAxis("Right_thumbX")));

            if(fireDirection != Vector3.zero)
            {
                lastFireTime = currentTime + fireWaitTime * 1000;
                carrotRB.MoveRotation(Quaternion.LookRotation(fireDirection));
                carrotRB.useGravity = true;
                carrotRB.centerOfMass = new Vector3(10f, 10f, 10f);
                carrotRB.AddForce(fireDirection.normalized * projectileForce);
                spawnedCarrot.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                spawnedCarrot.GetChild(0).GetComponent<CarrotMovement>().fireProjectile();
                Physics.IgnoreCollision(spawnedCarrot.GetChild(0).GetComponent<Collider>(), GetComponent<Collider>());

                //spawnedCarrot.transform.rotation = Quaternion.LookRotation(direction);                
                spawnedCarrot = null;
            } else
            {
                Destroy(spawnedCarrot.gameObject);
                spawnedCarrot = null;
            }
            
        }
	}
    
}

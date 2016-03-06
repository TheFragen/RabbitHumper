using UnityEngine;
using XboxCtrlrInput;
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
	private string playerString;
    XboxController controller;
    Vector3 upFix = new Vector3(0, 1.2f, 0);
    float letGoTime;


    // Use this for initialization
    void Start () {
		playerString = GetComponent<PlayerMovement>().getPlayerNumb().ToString();
        switch (GetComponent<PlayerMovement>().getPlayerNumb())
        {
            case 1:
                controller = XboxController.First;
                break;
            case 2:
                controller = XboxController.Second;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (AmmoController.instance.stillAmmo (GetComponent<PlayerMovement> ().getPlayerNumb())) {
			float currentTime = Time.time * 1000;
            Vector3 direction = Vector3.Normalize (new Vector3 (0, XCI.GetAxis(XboxAxis.RightStickY, controller), XCI.GetAxis(XboxAxis.RightStickX, controller)));


			//Show carrot when player holds down R2
			if (XCI.GetButtonDown(XboxButton.RightBumper, controller) && spawnedCarrot == null && currentTime > lastFireTime) {
				spawnedCarrot = Instantiate (carrot, this.transform.position + upFix, Quaternion.Euler (new Vector3 (10, 0, 0))) as Transform;
				carrotRB = spawnedCarrot.GetComponent<Rigidbody> ();
                spawnedCarrot.GetChild(0).GetComponent<BoxCollider>().enabled = false;
			}
        
			//"Animate" when the player rotates the right thumb stick
			if (spawnedCarrot != null && XCI.GetButton(XboxButton.RightBumper, controller)) {
				carrotRB.useGravity = false;
				if (direction.magnitude == 0) {
					spawnedCarrot.position = this.transform.position + upFix;
					if (lastDirection.magnitude > 0) {
						carrotRB.MoveRotation (Quaternion.LookRotation (lastDirection));
					} else {
						carrotRB.MoveRotation (Quaternion.Euler (0, 0, 0));
					}

				} else {
					spawnedCarrot.position = this.transform.position + upFix;
					carrotRB.MoveRotation (Quaternion.LookRotation (direction));
					lastDirection = direction;
				}
			}

			//Fire the carrot
			if (XCI.GetButtonUp(XboxButton.RightBumper, controller) && spawnedCarrot != null) {
                Vector3 fireDirection = Vector3.Normalize (new Vector3 (0, XCI.GetAxis(XboxAxis.RightStickY, controller), XCI.GetAxis(XboxAxis.RightStickX, controller)));

				if (fireDirection != Vector3.zero) {
					lastFireTime = currentTime + fireWaitTime * 1000;
					carrotRB.MoveRotation (Quaternion.LookRotation (fireDirection));
					carrotRB.useGravity = true;
					carrotRB.centerOfMass = new Vector3 (10f, 10f, 10f);
					carrotRB.AddForce (fireDirection.normalized * projectileForce);
					spawnedCarrot.GetChild (0).GetComponent<BoxCollider> ().enabled = true;
					spawnedCarrot.GetChild (0).GetComponent<CarrotMovement> ().fireProjectile ();
					Physics.IgnoreCollision (spawnedCarrot.GetChild (0).GetComponent<Collider> (), GetComponent<Collider> ());
                    AmmoController.instance.subAmmo(this.transform.GetComponent<PlayerMovement>().getPlayerNumb());

                    //spawnedCarrot.transform.rotation = Quaternion.LookRotation(direction);                
                    spawnedCarrot = null;
				} else {
					Destroy (spawnedCarrot.gameObject);
					spawnedCarrot = null;
				}
            
			}
		}
	}
    
}

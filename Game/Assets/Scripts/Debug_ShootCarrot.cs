using UnityEngine;
using System.Collections;

public class Debug_ShootCarrot : MonoBehaviour {
    public Transform carrot;
    public Transform spawnedCarrot;
    Rigidbody carrotRB;
    Vector3 lastDirection = Vector3.zero;
    public float projectileForce;
    float lastFireTime = 0;
    public float fireWaitTime = 0;
    public Vector3 shootDirection;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.time * 1000;

        if(currentTime > lastFireTime)
        {
            spawnedCarrot = Instantiate(carrot, this.transform.position, Quaternion.identity) as Transform;
            carrotRB = spawnedCarrot.GetComponent<Rigidbody>();


            lastFireTime = currentTime + fireWaitTime * 1000;
            carrotRB.MoveRotation(Quaternion.LookRotation(shootDirection));
            carrotRB.useGravity = true;
            carrotRB.centerOfMass = new Vector3(10f, 10f, 10f);
            carrotRB.AddForce(shootDirection.normalized * projectileForce);
            spawnedCarrot.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            spawnedCarrot.GetChild(0).GetComponent<CarrotMovement>().fireProjectile();
            Physics.IgnoreCollision(spawnedCarrot.GetChild(0).GetComponent<Collider>(), GetComponent<Collider>());
            lastFireTime = currentTime + fireWaitTime * 1000;

        }

    }

    void FixedUpdate()
    {
       
    }
}

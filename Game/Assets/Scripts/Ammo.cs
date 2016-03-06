using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {


	public float worldRotationSpeed = 20.0f;
	public float localRotationSpeed = 10.0f;
	public float bounce = 0.001f;
	public float bounceClamp = 0.2f;
	private float accu = 0.0f;
	private int index;

	void Start(){
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.up, worldRotationSpeed * Time.deltaTime, Space.World);

		if (Mathf.Abs(accu) >= bounceClamp) {
			bounce *= -1.0f;
		}
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + bounce, gameObject.transform.position.z);
		accu += bounce;
	}

	public void setIndex(int i){
		index = i;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player1")){
			AmmoController.instance.addAmmo (1);
			DestoryAmmo ();
		}
		else if(other.CompareTag("Player2")){
			AmmoController.instance.addAmmo (2);
			DestoryAmmo ();
		}
	}


	void DestoryAmmo(){
		AmmoSpawner.instance.setSpaceOccupied(index);
		Destroy (gameObject);
	}

}

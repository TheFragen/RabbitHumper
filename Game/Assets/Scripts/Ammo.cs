using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {


	public float worldRotationSpeed = 20.0f;
	public float localRotationSpeed = 10.0f;
	private int index;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.up, worldRotationSpeed * Time.deltaTime, Space.World);
		gameObject.transform.Rotate (Vector3.up, localRotationSpeed * Time.deltaTime, Space.Self);
	}

	public void setIndex(int i){
		index = i;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player1")){
			AmmoController.instance.addAmmo (0);
			DestoryAmmo ();
		}
		else if(other.CompareTag("Player2")){
			AmmoController.instance.addAmmo (1);
			DestoryAmmo ();
		}
	}


	void DestoryAmmo(){
		AmmoSpawner.instance.setSpaceOccupied(index);
		Destroy (gameObject);
	}

}

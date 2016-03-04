using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {


	public float worldRotationSpeed = 20.0f;
	public float localRotationSpeed = 10.0f;
	private int index;


	/***  TEST VARIABLES ***/
	public float testTime = 10.0f;
	private float testLifeTimer;


	// Use this for initialization
	void Start () {
		testLifeTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.up, worldRotationSpeed * Time.deltaTime, Space.World);
		gameObject.transform.Rotate (Vector3.up, localRotationSpeed * Time.deltaTime, Space.Self);
		if (Time.time >= testLifeTimer + testTime) {
			AmmoSpawner.instance.setSpaceOccupied(index);
			Destroy (gameObject);
		}

	}

	public void setIndex(int i){
		index = i;
	}

}

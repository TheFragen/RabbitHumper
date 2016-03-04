using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour {


	public float angularSpeed = 20.0f;
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
		gameObject.transform.Rotate (Vector3.up, angularSpeed * Time.deltaTime, Space.World);
		if (Time.time >= testLifeTimer + testTime) {
			ObjectiveSpawnerManager.instance.setSpaceOccupied(index);
			Destroy (gameObject);
		}

	}

	public void setIndex(int i){
		index = i;
	}

}

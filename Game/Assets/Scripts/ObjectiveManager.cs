using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour {

	private int index;


	/***  TEST VARIABLES ***/
	public float testTime = 2.0f;
	private float testLifeTimer;


	// Use this for initialization
	void Start () {
		testLifeTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time >= testLifeTimer + testTime) {
			//ObjectiveSpawnerManager.instance.setSpaceOccupied(index);
			//Destroy (gameObject);
		}

	}

	public void setIndex(int i){
		index = i;
	}

}

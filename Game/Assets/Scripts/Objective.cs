using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {


	/**** TEST VARIABLES ****/
	public float testTimer = 10.0f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time >= timer + testTimer) {
			ObjectiveSpawner.instance.setObjAct ();
			Destroy (gameObject);
		}
	}
}

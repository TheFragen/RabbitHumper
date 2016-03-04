using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveSpawner : MonoBehaviour {
	public static ObjectiveSpawner instance = null;

	public GameObject objective;
	public List<GameObject> spawnPoints = new List<GameObject>();

	public float minTime = 2.0f;
	public float maxTime = 4.0f;

	private float startTime;
	private float spawnTimer;
	private int lastIndex;
	private bool objActive = false;

	void Awake(){
		//Makes the manager a singleton which persists through scenes
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}


	// Use this for initialization
	void Start () {
		startTime = Time.time;
		spawnTimer = Random.Range (minTime, maxTime);
		lastIndex = spawnPoints.Count + 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (!objActive) {
			if (Time.time >= startTime + spawnTimer) {
				int index = Random.Range (0, spawnPoints.Count);

				if (index != lastIndex) {
					Instantiate (objective, spawnPoints [index].transform.position, objective.transform.rotation);
					lastIndex = index;
					objActive = true;
				}
			}
		}
	}


	public void setObjAct(){
		objActive = false;
		startTime = Time.time;
		spawnTimer = Random.Range (minTime, maxTime);
	}
}

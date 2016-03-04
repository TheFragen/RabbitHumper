using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveSpawnerManager : MonoBehaviour {
	public static ObjectiveSpawnerManager instance = null;

	public GameObject objective;
	public List<GameObject> spawnPoints = new List<GameObject>();

	public float minTime = 2.0f;
	public float maxTime = 4.0f;

	private float startTime;
	private float spawnTimer;
	private int lastIndex;
	private bool[] spaceOccupied;

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
		spaceOccupied = new bool[spawnPoints.Count];
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time >= startTime + spawnTimer) {
			int index = Random.Range (0, spawnPoints.Count);

			if (index != lastIndex && spaceOccupied[index] == false) {
				GameObject newObj = Instantiate (objective, spawnPoints [index].transform.position, Quaternion.identity) as GameObject;
				newObj.GetComponent<ObjectiveManager>().setIndex (index);
				spaceOccupied [index] = true;
				lastIndex = index;
				startTime = Time.time;
			}
		}
	
	}

	public void setSpaceOccupied(int i){
		spaceOccupied [i] = false;
	}
}

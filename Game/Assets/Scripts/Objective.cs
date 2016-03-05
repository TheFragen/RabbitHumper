using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	[HideInInspector] bool objActive = false;
	public GameObject kid;
	public float duration = 5.0f;
	public int numberOfKids = 6;

	private bool player1Active = false;
	private bool player2Active = false;
	private bool player1Using = false;
	private bool player2Using = false;

	private float startTimer;
	private float durationPerKid;
	private float kidTimer;

	private GameObject player1;
	private GameObject player2;


	// Use this for initialization
	void Start () {	
		durationPerKid = duration / numberOfKids;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) { //A on x-box controller
			Debug.Log ("Fire1");
		}
		if (Input.GetButton ("Fire2")) { //B on x-box controller
			Debug.Log ("Fire2");
		}
		if (Input.GetButton ("Fire3")) { // X on x-box controller
			Debug.Log ("Fire3");
		}
	
		if (player1Active && Input.GetButton ("Fire3") && !objActive) {
			objActive = true;
			player1Using = true;
			startTimer = Time.time;
			kidTimer = startTimer + durationPerKid;
		} 
		else if (player2Active && Input.GetButton ("Fire3") && !objActive) {
			objActive = true;
			player2Using = true;
			startTimer = Time.time;
			kidTimer = startTimer + durationPerKid;
		} 

		if (objActive) {
			float curTime = Time.time;

			if (player1Using && Input.GetButton ("Fire3")) {
				if (curTime >= kidTimer) {
					spawnKid (player1);
				}
			} 
			else if (player2Using && Input.GetButton ("Fire3")) {
				if (curTime >= kidTimer) {
					spawnKid (player2);
				}
			} 
			else {
				objActive = false;
			}

			if (curTime >= startTimer + duration) {
				DestroyObjective ();
			}
		}

		//If the objective was being used but the player isnt holding the button anymore.
		if (!objActive && (player1Using || player2Using)) {
			DestroyObjective ();
		}

	}


	void spawnKid(GameObject player){
		kidTimer += durationPerKid;
		GameObject newKid = Instantiate (kid, gameObject.transform.position, Quaternion.identity) as GameObject;
		newKid.GetComponent<Kid> ().setColor (player.GetComponent<MeshRenderer> ().material.color);
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player1")) {
			player1Active = true;
			player1 = other.gameObject;
		}
		if(other.CompareTag("Player2")){
			player2Active = true;
			player2 = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player1")) {
			player1Active = false;
			player1Using = false;
		}
		if (other.CompareTag ("Player2")) {
			player2Active = false;
			player2Using = false;
		}
	}



	void DestroyObjective(){
		objActive = false;
		player1Active = false;
		player2Active = false;
		player1Using = false;
		player2Using = false;
		ObjectiveSpawner.instance.setObjAct ();
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class camWin : MonoBehaviour {

	public float timer = 5.0f;
	public float dist = 7.0f;
	public float zoomDur = 2.0f;
	private bool gameWon = false;
	private bool zoom = false;
	private float startTimer;
	private Vector3 camPos;
	private Vector3 playerPos;
	private Vector3 finalPos;
	private GameObject winner;
	private Vector3 offset = new Vector3(0.0f,0.5f,0.0f);

	void Start(){
		camPos = Camera.main.transform.position;
		gameWon = false;
		zoom = false;
	}

	public void zoomIn(GameObject player){
		winner = player;
		startTimer = Time.time;
		gameWon = true;
	}

	void Update(){
		if (gameWon) {

			if (Time.time >= (startTimer + timer)) {
				Debug.Log ("LOADSCENE");
				SceneManager.LoadScene(0);
			} else {
				float t = (Time.time - startTimer) / zoomDur;
				Debug.Log ("MOVECAMERA");
				if (t < 1.0f) {
					Vector3 movement = Vector3.Lerp (camPos, winner.transform.position+offset, t);
					if (Vector3.Distance (movement, winner.transform.position+offset) > dist) {
						Camera.main.transform.position = movement;
					} else {
						finalPos = Camera.main.transform.position-winner.transform.position;
					}
				} else {
					Camera.main.transform.position = winner.transform.position + finalPos;
				}
			}
		}
	}
}

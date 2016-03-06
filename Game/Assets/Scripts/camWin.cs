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
	private bool stopped = false;

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
				SceneManager.LoadScene(1);
			} else {
				float t = (Time.time - startTimer) / zoomDur;
				Debug.Log ("MOVECAMERA");
				if (t < 1.0f) {
					Camera.main.transform.position = Vector3.Lerp (camPos, winner.transform.position+ new Vector3(dist,0.0f,0.0f), t);
					} else {
					if (!stopped) {
						finalPos = Camera.main.transform.position - winner.transform.position;
						stopped = true;
					}
					Camera.main.transform.position = winner.transform.position + finalPos;
				}
			}
		}
	}
}

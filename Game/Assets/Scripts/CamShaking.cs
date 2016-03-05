using UnityEngine;
using System.Collections;

public class CamShaking : MonoBehaviour {

	private Camera main;
	private Vector3 camPos;
	private bool camShaking = false;
	private bool movingBack = false;
	private float camTimer;
	public float camShakeDur = 1.0f;
	public float shakeWeight = 0.05f;
	private float moveBackTime = 0.5f;
	private float moveBackStart;


	// Use this for initialization
	void Start () {
		main = Camera.main;
		camPos = main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (camShaking) {
			if (Time.time >= camTimer + camShakeDur) {
				if (!movingBack) {
					moveBackStart = Time.time;
					movingBack = true;
				}
				float t = (Time.time - moveBackStart) / moveBackTime;
				main.transform.position = Vector3.Lerp (main.transform.position, camPos, t);

				if (t > 1.0f) {
					camShaking = false;
					movingBack = false;
				}
			} 
			else {
				main.transform.position += Random.insideUnitSphere * shakeWeight;
			}
		}
	}

	public void cameraShake(){
		camShaking = true;
		camTimer = Time.time;
	}
}

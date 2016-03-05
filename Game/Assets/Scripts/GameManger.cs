using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {
	public static GameManger instance = null;

	public int maxScore = 6;
	public int numbPlayers = 2;
	private int[] playerScores;


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
		playerScores = new int[numbPlayers];
		for (int i = 0; i < playerScores.Length; i++) {
			playerScores [i] = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < playerScores.Length; i++) {
			if (playerScores [i] >= maxScore) {
				Debug.Log ("Player " + (i + 1) + " Won");
			}
		}
	}

	/**
	 * EXPECTS THE ACTUAL PLAYER NUMBER i.e. 1 FOR PLAYER 1, 2 FOR PLAYER 2 etc.
	 */
	public void addScore(int index){
		playerScores [index - 1]++;
		Debug.Log("Player " + index + " Score: " + playerScores[index-1]);
	}
}

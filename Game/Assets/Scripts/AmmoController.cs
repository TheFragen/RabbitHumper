using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoController : MonoBehaviour {
	public static AmmoController instance = null;


	private int[] ammoCount;

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
		ammoCount = new int[GameManger.instance.numbPlayers];
	}


	/**
	 * EXPECTS THE ACTUAL PLAYER NUMBER i.e. 1 FOR PLAYER 1, 2 FOR PLAYER 2 etc.
	 */
	public void addAmmo(int index){
		ammoCount [index-1]++;
	}

	/**
	 * EXPECTS THE ACTUAL PLAYER NUMBER i.e. 1 FOR PLAYER 1, 2 FOR PLAYER 2 etc.
	 */
	public void subAmmo(int index){
		ammoCount [index-1]--;
	}
}

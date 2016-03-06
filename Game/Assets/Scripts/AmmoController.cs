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
        
	}

	// Use this for initialization
	void Start () {
		ammoCount = new int[GameManger.instance.numbPlayers];
		for (int i = 0; i < ammoCount.Length; i++) {
			ammoCount [i] = 5;
		}
	}


	/**
	 * EXPECTS THE ACTUAL PLAYER NUMBER i.e. 1 FOR PLAYER 1, 2 FOR PLAYER 2 etc.
	 */
	public void addAmmo(int index){
		ammoCount [index-1]++;
	}

    public int getAmmo(int playerID)
    {
        return ammoCount[playerID - 1];
    }

	/**
	 * EXPECTS THE ACTUAL PLAYER NUMBER i.e. 1 FOR PLAYER 1, 2 FOR PLAYER 2 etc.
	 */
	public void subAmmo(int index){
		ammoCount [index-1]--;
	}

	/**
	 * EXPECTS THE ACTUAL PLAYER NUMBER i.e. 1 FOR PLAYER 1, 2 FOR PLAYER 2 etc.
	 */
	public bool stillAmmo(int index){
		return ammoCount [index - 1] != 0;
	}
}

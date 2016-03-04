using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoController : MonoBehaviour {
	public static AmmoController instance = null;

	public int NoPlayers = 2;

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
		ammoCount = new int[NoPlayers];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addAmmo(int index){
		ammoCount [index-1]++;
	}

	public void subAmmo(int index){
		ammoCount [index-1]--;
	}
}

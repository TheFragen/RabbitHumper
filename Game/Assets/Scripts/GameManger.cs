using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManger : MonoBehaviour {
	public static GameManger instance = null;

	public List<GameObject> spawnPoints = new List<GameObject>();
	public int maxScore = 6;
	[HideInInspector] public int numbPlayers;
	public List<GameObject> players = new List<GameObject>();
	private int[] playerScores;
    public Material[] blueMats = new Material[3];
    public Material[] redMats = new Material[3];
 //   public bool soundEnabled = true;
    public bool musicEnabled = true;

	private bool gameOver = false;


    void Awake(){
		//Makes the manager a singleton which persists through scenes
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
        numbPlayers = players.Count;
		gameOver = false;
        musicEnabled = true;
    }

	// Use this for initialization
	void Start () {
		players.Capacity = numbPlayers;
		playerScores = new int[numbPlayers];
		for (int i = 0; i < playerScores.Length; i++) {
			playerScores [i] = 0;
		}
		spawnPlayer ();
	}

	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			for (int i = 0; i < playerScores.Length; i++) {
				if (playerScores [i] >= maxScore) {
					Debug.Log (i + 1);
					Camera.main.gameObject.GetComponent<camWin> ().zoomIn (GameObject.FindGameObjectWithTag ("Player" + (i + 1)));
					gameOver = true;
				}
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

    public int getPlayerScores(int playerID)
    {
        return playerScores[playerID - 1];
    }

	void spawnPlayer(){
		for (int i = 0; i < players.Count; i++) {
			int index = Random.Range (0, spawnPoints.Count);
			GameObject player = Instantiate (players [i], spawnPoints [index].transform.position, Quaternion.identity) as GameObject;
            player.gameObject.tag = "Player" + (i + 1);
			GameObject prev = spawnPoints [index];
			spawnPoints.Remove (prev);
			player.GetComponent<PlayerMovement> ().setPlayerNumb (i + 1);

            if (i < 1) //Blue
            {
                player.GetComponent<PlayerMovement>().setPlayerColor(blueMats);
            }
            else //Red
            {
                player.GetComponent<PlayerMovement>().setPlayerColor(redMats);
            }
        }
	}

    void OnGUI()
    {
        Event e = Event.current;

        //Toggle music - SHIFT + M
        if (e.type == EventType.KeyDown && e.shift && e.keyCode == KeyCode.M)
        {
            musicEnabled = !musicEnabled;
            string toggleString = (musicEnabled) ? "enabled" : "disabled";
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().enabled = musicEnabled;
            showStatusText("Music " + toggleString + ".");
        }

    /*    //Toggle sound - SHIFT + S
        if (e.type == EventType.KeyDown && e.shift && e.keyCode == KeyCode.S)
        {
            string toggleString = (soundEnabled) ? "enabled" : "disabled";
            showStatusText("Sound " + toggleString + ".");
        }*/


    }

    void showStatusText(string input)
    {
        Transform textGO = GameObject.Find("statusText").transform;
        
        Text statusText = textGO.GetComponent<Text>();
        statusText.text = input;
        statusText.enabled = true;

        StartCoroutine(waitAndDisable(2, textGO));
    }


    IEnumerator waitAndDisable(float waitTime, Transform reference)
    {
        yield return new WaitForSeconds(waitTime);
        reference.GetComponent<Text>().enabled = false;
    }
}

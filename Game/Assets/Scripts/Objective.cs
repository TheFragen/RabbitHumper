using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class Objective : MonoBehaviour {

	public GameObject kid;
	public float duration = 5.0f;
	public int numberOfKids = 6;

	public bool objActive = false;
	private bool player1Active = false;
	private bool player2Active = false;
	private bool player1Using = false;
	private bool player2Using = false;

	private float startTimer;
	private float durationPerKid;
	private float kidTimer;

	private GameObject player1;
	private GameObject player2;
    Animator anim;

    public AudioClip[] kidSpawnSound = new AudioClip[6];
    int kidSpawnSoundIndex = 0;
    public AudioClip spawnSound;

    public Material redBaby;
    public Material blueBaby;

	// Use this for initialization
	void Start () {	
		durationPerKid = duration / numberOfKids;
		objActive = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
        this.GetComponent<AudioSource>().PlayOneShot(spawnSound);
	}
	
	// Update is called once per frame
	void Update () {
		if (!objActive && (player1Active || player2Active)) {
			if (player1Active && XCI.GetButton(XboxButton.X,XboxController.First)) {
				player1Using = true;
                anim.SetBool("isHumping", true);
                if (!player1.transform.GetChild(0).GetComponent<Animator>().GetBool("isHumping"))
                {
                    player1.transform.GetChild(0).GetComponent<Animator>().SetBool("isHumping", true);
                }
                
                startObj ();
			} 
			else if (player2Active && XCI.GetButton(XboxButton.X, XboxController.Second))
            {
				player2Using = true;
                anim.SetBool("isHumping", true);
                if (!player2.transform.GetChild(0).GetComponent<Animator>().GetBool("isHumping"))
                {
                    player2.transform.GetChild(0).GetComponent<Animator>().SetBool("isHumping", true);
                }
                startObj ();
			} 
		}

		else if (objActive) {
			float curTime = Time.time;

			if (player1Using && XCI.GetButton(XboxButton.X, XboxController.First)) {
				if (curTime >= kidTimer) {
					spawnKid (player1);
				}
			} 
			else if (player2Using && XCI.GetButton(XboxButton.X, XboxController.Second)) {
				if (curTime >= kidTimer) {
					spawnKid (player2);
				}
			} 
			else {
				DestroyObjective ();
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

	void startObj(){
		objActive = true;
		startTimer = Time.time;
		kidTimer = startTimer + durationPerKid;
	}

	void spawnKid(GameObject player){
		GameManger.instance.addScore(player.GetComponent<PlayerMovement> ().getPlayerNumb());
		kidTimer += durationPerKid;
		GameObject newKid = Instantiate (kid, gameObject.transform.position, Quaternion.identity) as GameObject;
        if(kidSpawnSoundIndex < kidSpawnSound.Length)
        {
            GetComponent<AudioSource>().PlayOneShot(kidSpawnSound[kidSpawnSoundIndex++]);
        }


        Material[] _tmp = newKid.transform.GetComponent<Renderer>().materials;
        if (player.GetComponent<PlayerMovement>().playerNumber == 1) //Blue
        {
            _tmp[3] = blueBaby;
        } else
        {
            _tmp[3] = redBaby;
        }

        newKid.transform.GetComponent<Renderer>().materials = _tmp;
        //	newKid.GetComponent<Kid> ().setColor (player.GetComponent<MeshRenderer> ().material.color);
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
            player1.transform.GetChild(0).GetComponent<Animator>().SetBool("isHumping", false);
        }
		if (other.CompareTag ("Player2")) {
			player2Active = false;
			player2Using = false;
            player1.transform.GetChild(0).GetComponent<Animator>().SetBool("isHumping", false);
        }
	}

	void DestroyObjective(){
        if (player1 != null)
        {
            player1.transform.GetChild(0).GetComponent<Animator>().SetBool("isHumping", false);
        }
        else if(player2 != null)
        {
            player2.transform.GetChild(0).GetComponent<Animator>().SetBool("isHumping", false);
        }
        objActive = false;
		player1Active = false;
		player2Active = false;
		player1Using = false;
		player2Using = false;
		ObjectiveSpawner.instance.setObjAct ();
		Destroy (gameObject);
	}
}

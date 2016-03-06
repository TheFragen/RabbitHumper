using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using UnityEngine.UI;

public class MenuActionHandler : MonoBehaviour {

	XboxController controller1;
	XboxController controller2;

	public Text Text1, Text2;

	bool player1 = false;
	bool player2 = false;

	// Use this for initialization
	void Awake () {
		player1 = false;
		player2 = false;
		controller1 = XboxController.First;
		controller2 = XboxController.Second;
        Text1.color = Color.white;
        Text2.color = Color.white;
        Text1.text = "Player 1 press 'A'";
		Text2.text = "Player 2 press 'A'";
	}
	
	// Update is called once per frame
	void Update () {

		if (!player1 && XCI.GetButton (XboxButton.A, controller1)) {
			player1 = true;
            Text1.color = Color.green;
            Text1.text = "Player 1 ready";
		}
		if (!player2 && XCI.GetButton (XboxButton.A, controller2)) {
			player2 = true;
            Text2.color = Color.green;
			Text2.text = "Player 2 ready";
		}
			
		if (player1 && player2 || Input.GetKey(KeyCode.Return)) {
			loadLevel (1);
		}
	
	}

    public void loadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
}

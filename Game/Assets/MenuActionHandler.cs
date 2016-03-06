using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using UnityEngine.UI;

public class MenuActionHandler : MonoBehaviour {

	XboxController controller1;
	XboxController controller2;

	public Text Text1, Text1P, Text2, Text2P;

	bool player1 = false;
	bool player2 = false;

	// Use this for initialization
	void Awake () {
		controller1 = XboxController.First;
		controller2 = XboxController.Second;
		Text1.transform.gameObject.SetActive (true);
		Text1P.transform.gameObject.SetActive (false);
		Text2.transform.gameObject.SetActive (true);
		Text2P.transform.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (!player1 && XCI.GetButton (XboxButton.A, controller1)) {
			player1 = true;
			Text1.transform.parent.gameObject.SetActive (false);
			Text1P.transform.parent.gameObject.SetActive (true);
		}
		if (!player2 && XCI.GetButton (XboxButton.A, controller2)) {
			player2 = true;
			Text2.transform.parent.gameObject.SetActive (false);
			Text2P.transform.parent.gameObject.SetActive (true);
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

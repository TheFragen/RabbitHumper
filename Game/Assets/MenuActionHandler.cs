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
	public GameObject canvas;
	public float screenFader = 1.0f;
	float start;
	RawImage img;
	Color imgColor;
	bool loadLevelB = false;


	// Use this for initialization
	void Awake () {
		start = Time.time;	
		img = canvas.GetComponent<RawImage> ();
		imgColor = img.color;
		img.color = Color.black;
		loadLevelB = false;

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
		if(Time.time <= (start+screenFader)){
			float t = (Time.time -start)/screenFader;
			if (t < 1.0f) {
				img.color = Color.Lerp (img.color, imgColor, t);
				if (player1) {
					Text1.color = Color.Lerp (new Color (Text1.color.r, Text1.color.g, Text1.color.b, 0.0f), Color.green, t);
				} else {
					Text1.color = Color.Lerp (new Color (Text1.color.r, Text1.color.g, Text1.color.b, 0.0f), Color.white, t);
				}
				if (player2) {
					Text2.color = Color.Lerp (new Color (Text2.color.r, Text2.color.g, Text2.color.b, 0.0f), Color.green, t);
				} else {
					Text2.color = Color.Lerp (new Color (Text2.color.r, Text2.color.g, Text2.color.b, 0.0f), Color.white, t);
				}
			}
		}

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
			if(!loadLevelB){
				loadLevelB = true;
				start = Time.time;
			}
		}

        if (XCI.GetButton(XboxButton.Back))
        {
            Application.Quit();
        }
		if (loadLevelB) {
				float t = (Time.time - start)/screenFader;
				if (t <= 1.0f) {
				img.color = Color.Lerp (img.color, Color.black, t);
				Text1.color = Color.Lerp (Text1.color,new Color(Color.green.r,Color.green.g,Color.green.b, 0.0f), t);
				Text2.color = Color.Lerp (Text2.color, new Color(Color.green.r,Color.green.g,Color.green.b,0.0f), t);
				} else {
					loadLevel (2);
				}
		}
	}

    public void loadLevel(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }
}

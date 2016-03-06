using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinAnimationHandler : MonoBehaviour {
    public GameObject RedWinsGUI;
    public GameObject BlueWinsGUI;
    public GameObject StarGUI;
    public GameObject WinText;
    public GameObject InstructionsText;
    //  public GameObject Camera;
    GameObject wonPlayer;

    bool doOnce = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.transform.GetComponent<camWin>().getGameWon())
        {
            wonPlayer = Camera.main.transform.GetComponent<camWin>().getWinner();

            if (!doOnce)
            {
                if (wonPlayer.GetComponent<PlayerMovement>().playerNumber == 1) //Blue
                {
                    BlueWinsGUI.SetActive(true);
                    StartCoroutine(showStar(1.1f));
                    WinText.SetActive(true);
                    WinText.GetComponent<Text>().text = "Blue wins!";
                    InstructionsText.SetActive(true);
                }
                else
                {
                    RedWinsGUI.SetActive(true);
                    StartCoroutine(showStar(1.1f));
                    WinText.SetActive(true);
                    WinText.GetComponent<Text>().text = "Red wins!";
                    InstructionsText.SetActive(true);
                }
                doOnce = true;
            }
            
        }

    }


    IEnumerator showStar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StarGUI.SetActive(true);
    }
}

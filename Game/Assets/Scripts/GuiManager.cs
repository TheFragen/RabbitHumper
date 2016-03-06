using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {
    private string blueAmmo;
    private string blueKid;
    private string redAmmo;
    private string redKid;

    public GameObject blueAmmoRef;
    public GameObject blueKidRef;
    public GameObject redAmmoRef;
    public GameObject redKidRef;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        blueAmmo = AmmoController.instance.getAmmo(1).ToString();
        redAmmo = AmmoController.instance.getAmmo(2).ToString();
        blueKid = GameManger.instance.getPlayerScores(1).ToString();
        redKid = GameManger.instance.getPlayerScores(2).ToString();

        blueAmmoRef.GetComponent<UnityEngine.UI.Text>().text = blueAmmo;
        redAmmoRef.GetComponent<UnityEngine.UI.Text>().text = redAmmo;

        blueKidRef.GetComponent<UnityEngine.UI.Text>().text = blueKid;
        redKidRef.GetComponent<UnityEngine.UI.Text>().text = redKid;
    }
}

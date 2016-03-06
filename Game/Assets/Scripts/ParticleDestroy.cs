using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {
    public int waitSeconds = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine("destroyAfterPlay");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator destroyAfterPlay()
    {
        yield return new WaitForSeconds(waitSeconds);
        Destroy(this);
    }
}

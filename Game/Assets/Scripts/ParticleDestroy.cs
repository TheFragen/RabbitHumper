using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {
    public float waitSeconds = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine(destroyAfterPlay(waitSeconds));
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator destroyAfterPlay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}

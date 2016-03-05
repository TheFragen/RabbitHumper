using UnityEngine;
using System.Collections;

public class PlayerAudioHandler : MonoBehaviour {
    new AudioSource audio;
    public AudioClip[] carrotShootSounds = new AudioClip[3];
    public AudioClip rabbitRunning;
    public AudioClip rabbitStopping;
    bool runPlaying = false;

    // Use this for initialization
    void Start () {
        audio = this.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playCarrotShoot() {
        int var = Random.Range(0, 3);
        Debug.Log(var);
        audio.PlayOneShot(carrotShootSounds[var]);
    }

    public void playRunning(){
        if(audio.clip != rabbitRunning)
        {
            audio.clip = rabbitRunning;
            audio.loop = true;
            audio.Play();
        }
        
    }

    public void playStopping()
    {
        if (audio.clip == rabbitRunning)
        {
            audio.Stop();
            audio.clip = rabbitStopping;
            audio.loop = false;
            audio.Play();
        }
    }
}

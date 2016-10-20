using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioSource BGM;
	public AudioClip musicaFinaliza;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);

		if (FindObjectsOfType<AudioManager> ().Length > 1) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeBGM(AudioClip music)
	{
		if (BGM.name == music.name) {
			return;
		} 
		else 
		{
			BGM.Stop ();
			BGM.clip = music;
			BGM.loop = true;
			BGM.Play ();
		}
	}

	public void FinalizaFase()
	{
		BGM.Stop ();
		BGM.loop = false;
		BGM.clip = musicaFinaliza;
		BGM.Play();
	}
}

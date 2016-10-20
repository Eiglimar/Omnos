using UnityEngine;
using System.Collections;

public class AudioManagerCheck : MonoBehaviour {

	public GameObject audioM;

	// Use this for initialization
	void Start () {
		if (FindObjectOfType<AudioManager> ()) {
			return;
		} 
		else {
			Instantiate (audioM, transform.position, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour {

	public GameObject pauseUI;
	private GameObject HUD;
	public bool pause;

	public void Start()
	{
		pause = false;
		HUD = GameObject.Find ("HUD");
	}

	public void Update()
	{
		if(Input.GetButtonDown("Pause"))
		{
			pause = !pause;
			if(pause) 
			{
				pauseUI.SetActive (true);
				HUD.SetActive (false);
				Time.timeScale = 0;
			} 
			else 
			{
				pauseUI.SetActive (false);
				HUD.SetActive (true);
				Time.timeScale = 1;	
			}
		}
	}

	public void LoadScene(string SceneName)
	{
		SceneManager.LoadScene (SceneName);
	}
}

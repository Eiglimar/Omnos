using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MorreuUI : MonoBehaviour {

	public GameObject HUD;
	public GameObject morreuUI;

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	public void AtivaMenuMorreu()
	{
		Time.timeScale = 0;
		morreuUI.SetActive (true);
		HUD.SetActive (false);
	}

	public void LoadScene(string SceneName)
	{
		SceneManager.LoadScene (SceneName);
	}

	public void Continuar()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
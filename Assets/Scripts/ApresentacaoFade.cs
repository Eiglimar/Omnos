using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ApresentacaoFade : MonoBehaviour 
{
	public Image splashImg;
	public Text txtApresentacao;
	public string loadLevel;

	//sempre que a cena for carregada volta o tempo normal do jogo
	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Level Loaded");
		Debug.Log(scene.name);
		Debug.Log(mode);
		Time.timeScale = 1;
	}

	IEnumerator Start()
	{
		splashImg.canvasRenderer.SetAlpha (0.0f);
		txtApresentacao.canvasRenderer.SetAlpha (0.0f);

		FadeIn ();
		yield return new WaitForSeconds(2.5f);
		FadeOut ();
		yield return new WaitForSeconds (2.5f);
		SceneManager.LoadScene (loadLevel);
	}

	void FadeIn()
	{
		splashImg.CrossFadeAlpha (1.0f, 1.5f, false);
		txtApresentacao.CrossFadeAlpha (1.0f, 1.5f, false);
	}

	void FadeOut()
	{
		splashImg.CrossFadeAlpha (0.0f,2.5f,false);
		txtApresentacao.CrossFadeAlpha (0.0f,2.5f,false);
	}
}

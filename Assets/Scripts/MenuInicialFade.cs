using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuInicialFade : MonoBehaviour {

	//Image
	public Image bgm;
	public Image gameName;

	//Button image
	public Button iniciar;
	public Button continuar;
	public Button sobre;
	public Button configuracao;

	//Text
	public Text iniciarTxt;
	public Text continuarTxt;
	public Text sobreTxt;
	public Text configuracaoTxt;

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
		//Image
		bgm.canvasRenderer.SetAlpha (0.0f);
		gameName.canvasRenderer.SetAlpha (0.0f);

		//Button Image
		iniciar.image.canvasRenderer.SetAlpha (0.0f);
		continuar.image.canvasRenderer.SetAlpha (0.0f);
		sobre.image.canvasRenderer.SetAlpha (0.0f);
		configuracao.image.canvasRenderer.SetAlpha (0.0f);

		//Text
		iniciarTxt.canvasRenderer.SetAlpha (0.0f);
		continuarTxt.canvasRenderer.SetAlpha (0.0f);
		sobreTxt.canvasRenderer.SetAlpha (0.0f);
		configuracaoTxt.canvasRenderer.SetAlpha (0.0f);

		FadeIn ();
		yield return new WaitForSeconds(2.5f);

	}

	void FadeIn()
	{
		//Image
		bgm.CrossFadeAlpha (1.0f, 2.0f, false);
		gameName.CrossFadeAlpha (1.0f, 2.0f, false);

		//Button Image
		iniciar.image.CrossFadeAlpha (1.0f, 2.0f, false);
		continuar.image.CrossFadeAlpha (1.0f, 2.0f, false);
		sobre.image.CrossFadeAlpha (1.0f, 2.0f, false);
		configuracao.image.CrossFadeAlpha (1.0f, 2.0f, false);

		//Text
		iniciarTxt.CrossFadeAlpha (1.0f, 2.0f, false);
		continuarTxt.CrossFadeAlpha (1.0f, 2.0f, false);
		sobreTxt.CrossFadeAlpha(1.0f, 2.0f, false);
		configuracaoTxt.CrossFadeAlpha (1.0f, 2.0f, false);
	}
}

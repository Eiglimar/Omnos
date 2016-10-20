using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour {

	public Button btnContinuar;
	public GameObject resetSavePanel;

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
		/*Debug.Log(scene.name);
		Debug.Log(mode);*/
		Time.timeScale = 1;
	}

	void Start()
	{
		//pega o botao do continue
		btnContinuar = GameObject.Find("btnContinuar").GetComponent<Button>();

		//checa se existem os dados ja do jogo salvo.
		if (PlayerPrefs.HasKey ("save")) 
		{
			btnContinuar.interactable = true;	
		} 
		else 
		{
			btnContinuar.interactable = false;
		}
	}

	void Update()
	{	
		
	}

	public void IniciarGame()
	{
		//resetSavePanel = GameObject.Find ("resetSave");

		if (PlayerPrefs.HasKey ("save")) 
		{
			resetSavePanel.SetActive (true);
		} 
		else 
		{
			//carrega dados do save
			PlayerPrefsX.SetBool ("save", true);
			PlayerPrefs.SetInt ("Player_VidaMaxima",100);
			PlayerPrefs.SetInt ("Player_ManaMaxima",100);
			PlayerPrefs.SetInt ("Player_Forca",1);
			PlayerPrefs.SetInt ("Player_Defesa",1);
			PlayerPrefs.SetInt ("Player_Nivel",1);
			PlayerPrefs.SetInt ("Player_Dinheiro", 0);
			PlayerPrefs.SetInt ("Player_Experiencia", 0);
			PlayerPrefs.SetInt ("Player_Experiencia_Prox_Nivel", 100);

			//chama cena de historia inicial!
			LoadScene ("HistoriaInicial");
		}
	}

	public void ContinuarGame()
	{
		//vai para tela de escolha de mundo das fases
		LoadScene ("MapaMundo");
	}

	public void ResetSave()
	{
		if (PlayerPrefs.HasKey ("save")) {
			PlayerPrefs.DeleteAll ();
			LoadScene ("MenuInicial");	
		} 
		else 
		{
			return;
		}
	}

	public void LoadScene(string SceneName)
	{
		SceneManager.LoadScene (SceneName);
	}
}

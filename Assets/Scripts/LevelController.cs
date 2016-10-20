using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public Transform[] fases = new Transform[5];
	public string mundoAtual;

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
		
		mundoAtual = scene.name;
	}

	// Use this for initialization
	void Start ()
	{
		ChecaNiveis ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	public void ChecaNiveis()
	{
		switch(mundoAtual)
		{
			case "Suddene":
				if (PlayerPrefs.HasKey ("SuddeneNiveis")) 
				{
					var niveisLiberados = PlayerPrefsX.GetIntArray ("SuddeneNiveis");
					for (int i = 0; i < fases.Length; i++)
					{
						if(i == 4)
						{
							if(niveisLiberados[i] == 1)
							{
								fases [i].Find("Ativo").gameObject.SetActive (true);
							}	
						}
						else
						{
							if(niveisLiberados[i] == 1)
							{
								fases [i].Find("Ativo").gameObject.SetActive (true);
								fases [i].Find("Desativo").gameObject.SetActive (false);
							}
						}
					}
				} 
				else 
				{
					int[] suddeneNiveis = {1,0,0,0,0};
					PlayerPrefsX.SetIntArray ("SuddeneNiveis",suddeneNiveis);
					ChecaNiveis ();
				}
			break;
		}
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}

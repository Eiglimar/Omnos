using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapaMundo : MonoBehaviour {

	public Transform[] mundos = new Transform[5];
	/*
	 * 0 - Suddene
	 * 1 - Dundeya
	 * 2 - Callistia
	 * 3 - Salistick
	 * 4 - Oldeom
	*/
	// Use this for initialization
	void Start ()
	{
		if (PlayerPrefs.HasKey ("mundosLiberados")) // se ja tem save vai ver quais mundos estao liberados
		{
			LiberaMundos ();
		} 
		else // se nao tiver mundosLiberados, criamos a variavel e setamos o primeiro mundo liberado.
		{
			int[] mundosLiberados = {1,0,0,0,0};
			PlayerPrefsX.SetIntArray ("mundosLiberados", mundosLiberados);
			LiberaMundos ();
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void LiberaMundos() // verifica os mundos liberados e os libera
	{
		var mundosLiberados = PlayerPrefsX.GetIntArray ("mundosLiberados");
		for (int i = 0; i < mundos.Length; i++)
		{
			if(mundosLiberados[i] == 1)
			{
				mundos [i].Find("Ativo").gameObject.SetActive (true);
				mundos [i].Find("Desativo").gameObject.SetActive (false);
			}

		}
	}



	public void LoadScene(string SceneName)
	{
		SceneManager.LoadScene (SceneName);
	}
}

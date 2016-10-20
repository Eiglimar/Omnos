using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetSave : MonoBehaviour {

	public GameObject panelResetDeactived,panelResetActived; //definir no inspector

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ResetarSave()
	{
		PlayerPrefs.DeleteAll ();
		LoadScene ("MenuInicial");
	}

	public void ChamaTelaReset()
	{
		if (PlayerPrefs.HasKey ("save")) {
			panelResetDeactived.SetActive (true);	
		} 
		else 
		{
			return;
		}
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}

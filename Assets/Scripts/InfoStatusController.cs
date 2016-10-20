using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoStatusController : MonoBehaviour {

	public string nivel, vidaMax, manaMax, forca, defesa, dinheiro,exp;
	public Text txtNivel, txtVida, txtMana, txtForca, txtDefesa, txtDinheiro, txtExp;

	// Use this for initialization
	void Start () 
	{
		vidaMax = PlayerPrefs.GetInt ("Player_VidaMaxima").ToString();
		manaMax = PlayerPrefs.GetInt ("Player_ManaMaxima").ToString();
		forca = PlayerPrefs.GetInt ("Player_Forca").ToString();
		defesa = PlayerPrefs.GetInt ("Player_Defesa").ToString();
		nivel = PlayerPrefs.GetInt ("Player_Nivel").ToString();
		dinheiro = PlayerPrefs.GetInt ("Player_Dinheiro").ToString();
		exp = PlayerPrefs.GetInt ("Player_Experiencia").ToString();

		txtNivel = GameObject.Find ("txtNivel").GetComponent<Text>();
		txtVida = GameObject.Find ("txtVidaMax").GetComponent<Text>();
		txtMana = GameObject.Find ("txtManaMax").GetComponent<Text>();
		txtForca = GameObject.Find ("txtForca").GetComponent<Text>();
		txtDefesa = GameObject.Find ("txtDefesa").GetComponent<Text>();
		txtDinheiro = GameObject.Find ("txtDinheiro").GetComponent<Text>();
		txtExp = GameObject.Find ("txtExperiencia").GetComponent<Text>();

		txtNivel.text = nivel;
		txtVida.text = vidaMax;
		txtMana.text = manaMax;
		txtForca.text = forca;
		txtDefesa.text = defesa;
		txtDinheiro.text = dinheiro;
		txtExp.text = exp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}

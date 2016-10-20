using UnityEngine;
using System.Collections;

public class AttackTriggerPersonagem : MonoBehaviour {

	public GameObject player;
	public int dano;
	public int danoPlayerPrefs;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Jogador");
		danoPlayerPrefs = PlayerPrefs.GetInt ("Player_Forca");
		dano = 30 * danoPlayerPrefs;
	}
	
	// Update is called once per frame
	void Update () {
		danoPlayerPrefs = PlayerPrefs.GetInt ("Player_Forca");
		dano = 30 * danoPlayerPrefs;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.isTrigger != true && col.CompareTag("Inimigo"))
		{
			col.SendMessageUpwards("Damage",dano);
		}
	}
}

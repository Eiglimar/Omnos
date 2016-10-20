using UnityEngine;
using System.Collections;

public class Agua : MonoBehaviour {

	public Personagem player;

	void Start()
	{
		//player = GameObject.FindGameObjectWithTag ("Player").GetComponentInParent<Personagem> ();
		player = GameObject.Find("Jogador").GetComponent<Personagem>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Player"))
		{
			player.Damage (PlayerPrefs.GetInt ("Player_VidaMaxima"));
			//StartCoroutine (player.KnockBack (0.02f, 150, player.transform.position));
			//player.Die();
		}
	}
}
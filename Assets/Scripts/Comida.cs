using UnityEngine;
using System.Collections;

public class Comida : MonoBehaviour {

	public string tipoRecupera;
	public int valorRecupera;
	public Personagem per;

	// Use this for initialization
	void Start () {
		per = GameObject.Find ("Jogador").GetComponent<Personagem>();
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			
			if(tipoRecupera == "Vida")
			{
				SoundFXController.PlaySound (soundFx.COMENDO_PERSONAGEM);
				per.RecuperaVida (valorRecupera);
			}
			else if(tipoRecupera == "Mana")
			{
				SoundFXController.PlaySound (soundFx.COMENDO_PERSONAGEM);
				per.RecuperaMana (valorRecupera);	
			}
			Destroy (this.gameObject);
		}
	}
}

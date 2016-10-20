using UnityEngine;
using System.Collections;

public class Dinheiro : MonoBehaviour {
	public int valor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			SoundFXController.PlaySound (soundFx.DINHEIRO);
			int valorAtual = PlayerPrefs.GetInt ("Player_Dinheiro");
			int valorNovo = valorAtual + valor;
			PlayerPrefs.SetInt("Player_Dinheiro",valorNovo);
			Destroy (this.gameObject);
		}
	}
}

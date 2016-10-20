using UnityEngine;
using System.Collections;

//parametros enum para pegar os sons (Marcados os indexes usados no array) definidos no inspector!!!!!
public enum soundFx {
	JUMP, //0
	DINHEIRO, //1
	ATAQUE_FISICO, //2
	DANO_PERSONAGEM, //3
	CAVEIRA_DAMAGE, // 4
	CAVEIRA_ATAQUE, // 5
	ATAQUE_ESPADA, // 6
	COMENDO_PERSONAGEM, // 7
	POTION_PERSONAGEM, // 8
	CAVEIRA_MAGICA_ATAQUE // 9
}

public class SoundFXController : MonoBehaviour {

	public AudioClip[] sonsFx; //definido no inspector
	public AudioSource audioFonte;
	public static SoundFXController instance;
	// Use this for initialization
	void Start () {
		audioFonte = GetComponent<AudioSource> ();
		instance = this;
	}

	public static void PlaySound(soundFx currentSound)
	{
		switch(currentSound)
		{
			case soundFx.JUMP:
				instance.audioFonte.PlayOneShot (instance.sonsFx[0]); //JUMP
			break;
			case soundFx.DINHEIRO:
				instance.audioFonte.PlayOneShot (instance.sonsFx[1]); //DINHEIRO
			break;
			case soundFx.ATAQUE_FISICO:
				instance.audioFonte.PlayOneShot (instance.sonsFx[2]); //ATAQUE FISICO
			break;
			case soundFx.DANO_PERSONAGEM:
				instance.audioFonte.PlayOneShot (instance.sonsFx[3]); // dano personagem
			break;
			case soundFx.CAVEIRA_DAMAGE:
				instance.audioFonte.PlayOneShot (instance.sonsFx[4]); // dano caveira1
			break;
			case soundFx.CAVEIRA_ATAQUE:
				instance.audioFonte.PlayOneShot (instance.sonsFx[5]); // caveira1 ataque
			break;
			case soundFx.ATAQUE_ESPADA:
				instance.audioFonte.PlayOneShot (instance.sonsFx[6]); // espada ataque
			break;
			case soundFx.COMENDO_PERSONAGEM:
				instance.audioFonte.PlayOneShot (instance.sonsFx[7]); // personagem comendo
			break;
			case soundFx.POTION_PERSONAGEM:
				instance.audioFonte.PlayOneShot (instance.sonsFx[8]); // personagem potion
			break;
			case soundFx.CAVEIRA_MAGICA_ATAQUE:
			instance.audioFonte.PlayOneShot (instance.sonsFx[9]); // ataque magia caveira
			break;
		}
	}
}

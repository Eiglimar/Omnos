using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteracaoFimFase : MonoBehaviour {

	public bool clicavel; // definir no inspector
	public int expObtida; // definir no inspector
	public string proxCena; // definir no inspector
	public Text txtExp,txtDinheiro,txtNivel; // definir no inspector
	public string mundo; //definir no inspector
	public GameObject faseFinalizarPanel; //definir no inspector
	public int defBonus,forcaBonus,vidaBonus,manaBonus; // definir no inspector
	public AudioManager theAM;

	// Use this for initialization
	void Start () 
	{
		theAM = FindObjectOfType<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// se definido como clicavel devera estar colidindo e apertar o ctrl para finalizar a fase!
		if (clicavel) {
			if (col.gameObject.tag == "Player") {
				if (Input.GetButtonDown ("Fire1")) {
					FinalizaFase ();
				}
			}	
		} 
		else // se definido que nao é clicavel, basta encostar no objeto que a fase será finalizada! 
		{
			if (col.gameObject.tag == "Player") 
			{
				FinalizaFase ();
			}
		}
	}

	//metodo de finalizacao!
	public void FinalizaFase()
	{
		//faz com que o panel da finalizacao fique ativo
		faseFinalizarPanel.SetActive (true);

		theAM.FinalizaFase ();

		//coloca o valor da experiencia obtido para o player ver a partir do valor definido aqui
		txtExp.text = expObtida.ToString();

		// coloca o valor do dinheiro que o player tem já somado com que ele pegou na fase!
		txtDinheiro.text = PlayerPrefs.GetInt ("Player_Dinheiro").ToString();

		//pega os dados de experiencia para fazer calcula para ver se vai upar!
		int expNecessariaProxLvl = PlayerPrefs.GetInt ("Player_Experiencia_Prox_Nivel");

		int expAnterior = PlayerPrefs.GetInt ("Player_Experiencia");

		int novaExp = expAnterior + expObtida;


		// armazena o exp obtido para as variaveis de controle global!
		PlayerPrefs.SetInt ("Player_Experiencia", novaExp);


		//Pega qual era a quantidade necessaria para upar!
		int expProxLevel = PlayerPrefs.GetInt ("Player_Experiencia");

		// se vc alcancou a quantidade necessaria ou ultrapassou, upa!
		if(expProxLevel >= expNecessariaProxLvl)
		{
			//pega os dados de estado do player para receber bonus de level up!
			int proxLvl = PlayerPrefs.GetInt ("Player_Nivel");
			int defesa = PlayerPrefs.GetInt ("Player_Defesa");
			int forca = PlayerPrefs.GetInt ("Player_Forca");
			int vida = PlayerPrefs.GetInt ("Player_VidaMaxima");
			int mana = PlayerPrefs.GetInt ("Player_ManaMaxima");

			//soma os valores bonus para o lvl up definidos pelo inspector!
			defesa += defBonus;
			forca += forcaBonus;
			vida += vidaBonus;
			mana += manaBonus;
			expNecessariaProxLvl += (expNecessariaProxLvl * 2);



			//acrescenta um lvl no jogador
			proxLvl += 1;

			//armazena na variavel global o novo lvl e novos status
			PlayerPrefs.SetInt ("Player_Nivel",proxLvl);
			PlayerPrefs.SetInt ("Player_Defesa",defesa);
			PlayerPrefs.SetInt ("Player_Forca",forca);
			PlayerPrefs.SetInt ("Player_VidaMaxima",vida);
			PlayerPrefs.SetInt ("Player_ManaMaxima",mana);
			PlayerPrefs.SetInt ("Player_Experiencia_Prox_Nivel",expNecessariaProxLvl);

		}

		// pega o nivel que o player esta, se ja upou ja vai mostrar o novo nivel!
		txtNivel.text = PlayerPrefs.GetInt ("Player_Nivel").ToString();

		//pega as fases do mundo que está jogando!
		int[] mundoNiveis = PlayerPrefsX.GetIntArray (mundo+"Niveis");

		//variavel para saber se acabou as fases do mundo atual!
		bool mundoFinalizado = false;

		// verificacao das fases do mundo!
		for(int i = 0; i < mundoNiveis.Length; i++)
		{
			//a prox fase que estiver com 0 vai ser liberada!
			if(mundoNiveis[i] == 0)
			{
				mundoNiveis[i] = 1;
				PlayerPrefsX.SetIntArray (mundo + "Niveis", mundoNiveis);
				break;
			}
			//se for a ultima interacao do for e nao encontrou 0, quer dizer que nao existem mais fases, logo acabou o mundo!
			if(mundoNiveis[i] == 1 && i == 4)
			{
				mundoFinalizado = true;	// seta que o mundo acabou!
			}
		}
			
		if(mundoFinalizado)//Se verificar que este mundo acabou cria o novo
		{
			int[] novoMundo = { 1, 0, 0, 0, 0 };
			PlayerPrefsX.SetIntArray (proxCena+"Niveis",novoMundo);
		}

		Time.timeScale = 0;

		//mundosLiberados


	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}

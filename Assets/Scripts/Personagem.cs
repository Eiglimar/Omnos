using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Personagem : MonoBehaviour {

	public int jumpHeight;
	public bool jump;
	public bool grounded;
	public bool walking;
	public bool wallCheck;
	public bool doubleJump;
	public Transform groundCheck; // arrasta transform!
	public LayerMask whatIsGround; // seleciona a layer ground!
	public Rigidbody2D rb2D;
	public Transform startPos;
	public Cam cam;
	public GameObject interacaoObj; // interação
	public bool facingRight;
	public float slide;
	public int moveSpeed;
	public Animator myAnimator;
	public bool attack1;
	public int dano;

	//pega o collider de ataque
	public Collider2D attackTrigger;
	private float attackTimer = 0;
	private float attackCd = 0.3f;

	//Variaveis para controlar a caixa de dialogo
	public GameObject caixaDialogo;
	public Text txtCaixaDialogo;
	public bool interagindo;
	public string txtInteracaoRecebido;

	//STATS
	//health
	public int currentHealth;
	public int maxHealth;
	//mana
	public int currentMana;
	public int maxMana;
	//forca
	public int forca;
	//defesa
	public int defesa;
	//dinheiro
	public int dinheiro;
	//experiencia
	public int exp;
	//nivel
	public int nivel;

	//UI Health and Stats
	public Image healthBar;
	public Image manaBar;

	//Morreu
	public MorreuUI morreuMenu;


	//piscar quando toma dano
	private Material mat;
	private Color[] colors = { Color.yellow, Color.red };

	// Use this for initialization
	//public override void  Start () {
	void Start()
	{
		//inicia os status do personagem!
		maxMana = PlayerPrefs.GetInt ("Player_ManaMaxima");
		maxHealth = PlayerPrefs.GetInt ("Player_VidaMaxima");
		nivel = PlayerPrefs.GetInt ("Player_Nivel");
		dinheiro = PlayerPrefs.GetInt ("Player_Dinheiro");
		forca = PlayerPrefs.GetInt ("Player_Forca");
		defesa = PlayerPrefs.GetInt ("Player_Defesa");
		exp = PlayerPrefs.GetInt ("Player_Experiencia");

		//pega o rigid body do player
		rb2D = GetComponent<Rigidbody2D> ();

		//velocidade do andar do personagem
		moveSpeed = 4;

		//altura que o persoagem pode pular
		jumpHeight = 250;

		//ele inicia olhando para a direita
		facingRight = true;

		doubleJump = false;

		//seta para nao estar atacando
		attack1 = false;

		//pega o material do sprite renderer do objeto personagem
		mat = GameObject.Find ("Personagem").GetComponent<SpriteRenderer> ().material;

		//instanciando a variavel do script Cam.cs
		cam = FindObjectOfType(typeof(Cam)) as Cam;
		transform.position = startPos.position;

		//inicia com o maximo de vída
		currentHealth = maxHealth;
		//inicia com o maximo de mana
		currentMana = maxMana;

		//inicia nao interagindo com nada
		interagindo = false;

		//dano inicial do player é de 30
		dano = 30 * forca;

		//pega o objeto do morreuController para fazer as acoes do painel quando morrer!
		morreuMenu = GameObject.Find ("morreuController").GetComponent<MorreuUI>();
	}

	void Awake()
	{
		attackTrigger.enabled = false;
	}

	// Update is called once per frame
	//public override void Update () 
	void Update()
	{
		//animacao do pulo!
		myAnimator.SetBool ("grounded",grounded);
		//myAnimator.SetFloat ("speedY",rb2D.velocity.y);
		//base.Update ();


		//se cair morreu
		if(transform.position.y <= -12.0f)
		{
			//this.Damage (maxHealth);
			Die();
			currentHealth = 0;
		}

		if (!wallCheck) 
		{   
			// verifica se nao esta colidindo com algum objeto na frente
			rb2D.velocity = new Vector2 (slide * moveSpeed, rb2D.velocity.y);// se eu deixar o y em 0 o personagem não vai pular
			//Debug.Log ("Não está batendo");
		}

		//caminhar
		slide = Input.GetAxis ("Horizontal");
		if (slide != 0) 
		{
			walking = true;
		}
		else 
		{
			walking = false;
		}

		if(walking)
		{
			Vector2 moveDir = new Vector2 (slide * moveSpeed, rb2D.velocity.y);
			rb2D.velocity = moveDir;
			flip (slide);
		}

		myAnimator.SetBool ("walk",walking);


		//para fazer interacao de informacoes e npcs
		if(Input.GetButtonDown("Fire1") && interacaoObj != null)
		{
			//Debug.Log ("Aqui tem alguma interacao...");

			//txtInteracaoRecebido = interacaoObj.SendMessage ("Interacao",SendMessageOptions.DontRequireReceiver); // nome do metodo de interacao e opcao(opcional porém importante)
			if (interacaoObj != null) {
				txtInteracaoRecebido = interacaoObj.GetComponent<InteracaoDialog> ().Interacao ();
				txtCaixaDialogo.text = txtInteracaoRecebido;
				//Controle de aparicao da caixa de dialogo
				caixaDialogo.SetActive (interagindo);

				if (interagindo) {
					interagindo = false;	
				} 
				else 
				{
					interagindo = true;
				}

			} 
			else 
			{
				interagindo = false;
			}

		}

		//se nao tiver mais no objeto de interacao a caixa de dialogo fica desativada!
		if(interacaoObj == null)
		{
			caixaDialogo.SetActive(false);	
		}

		if(Input.GetKeyDown(KeyCode.C) && !attack1)
		{
			SoundFXController.PlaySound (soundFx.ATAQUE_ESPADA);
			attack1 = true;
			attackTimer = attackCd;
			attackTrigger.enabled = true;
		}

		if (attack1) 
		{
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} 
			else 
			{
				attack1 = false;
				attackTrigger.enabled = false;
			}
		}

		myAnimator.SetBool ("attack1",attack1);

		//preencher a barra de vida
		healthBar.fillAmount = (float)currentHealth / maxHealth;

		//controla a quantidade de vida
		if(currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		// se nao tiver mais vida morre
		if (currentHealth <= 0) {
			Die ();
		}

		//preencher a barra de mana
		manaBar.fillAmount = (float)currentMana / maxMana;

		//controla a quantidade de vida
		if(currentMana > maxMana)
		{
			currentMana = maxMana;
		}
		// se nao tiver mais mana deixa zerada!
		if (currentMana <= 0) {
			currentMana = 0;
		}
	}

	public int GetDano()
	{
		return dano;
	}

	void FixedUpdate()
	{
		//pular
		jump = Input.GetButtonDown ("Jump");

		//pega se estiver na layer Ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);


		//se tiver pulando e estiver no chao e nao tiver dando o double jump
		if(jump && (grounded || !doubleJump))
		{
			//som do pulo
			SoundFXController.PlaySound (soundFx.JUMP);

			rb2D.velocity = new Vector2 (0, 0); // anula a velocidade que o personagem está caindo e assim o segundo pulo tera uma forca controlada de impulso
			rb2D.AddForce( new Vector2(0,jumpHeight)); // aplica a forca no eixo y para que o personagem "pule"

			//se tiver no ar e nao tiver dado o double jump pode dar o double jump!
			if(!grounded && !doubleJump)
			{
				doubleJump = true;
			}
		}
		else if(grounded)// se tiver no chao desabilita o double jump!
		{
			doubleJump = false;
		}


		//ajusta a camera para verificar se o persoagem acabou de nascer
		if(startPos.position == transform.position)
		{
			//morreu = false;
			cam.transform.position = new Vector3 (transform.position.x, cam.transform.position.y, cam.transform.position.z);
		}
	}

	public void flip(float slide)
	{
		if(slide > 0 && !facingRight || slide < 0 && facingRight)
		{
			facingRight = !facingRight;

			transform.localScale = new Vector3 (transform.localScale.x * -1, 1, 1);
		}
	}

	//desenha circulo para verificar o groundCheck
	void OnDrawGizmos()
	{
		//Color of gizmos is blue.
		Gizmos.color = Color.blue;
		//Gizmos is to draw a wire sphere using the grounder.transform's position, and the radius value.
		Gizmos.DrawWireSphere(groundCheck.position, 0.3f);
	}

	/*void OnCollisionEnter2D(Collision2D col)
	{
		switch(col.gameObject.tag)
		{
			case "Inimigo":
				if (attack1)
				{
					col.gameObject.GetComponent<Caveira1> ().Damage (dano);
				}
			break;
		}
	}*/

	/*void OnCollisionStay2D(Collision2D col)
	{
		switch(col.gameObject.tag)
		{
			case "Inimigo":
				if (attack1)
				{
					col.gameObject.GetComponent<Caveira1> ().Damage (dano);
				}
			break;
		}
	}*/

	void OnTriggerEnter2D(Collider2D col) // ao ficar colidindo em um trigger
	{
		
		switch (col.tag)
		{
			case "interacaoDialog":
				interacaoObj = col.gameObject;
			break;

			/*case "Inimigo":
				if (attack1)
				{
					SoundFXController.PlaySound (soundFx.ATAQUE_FISICO);
					if(col.name == "skull_1")
					{
						col.gameObject.GetComponent<Caveira1> ().Damage (dano);	
					}
					else if(col.name == "skull_2")
					{
						col.gameObject.GetComponent<Caveira2> ().Damage (dano);
					}
				}
			break;*/

			default:
			break;
		}
		wallCheck = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		switch(col.tag)
		{
			case "interacaoDialog":
				interacaoObj = null;
				txtCaixaDialogo.text = "Sem interação";
			break;
		}
		wallCheck = false;
	}

	//Funcao para fazer o personagem piscar quando for atacado!
	IEnumerator Flash(float time, float intervalTime)
	{
		float elapsedTime = 0f;
		int index = 0;
		while(elapsedTime < time )
		{
			//altera as cores entre amarelo e vermelho!(ou nas cores que estiverem no array!)
			mat.color = colors[index % 2];

			elapsedTime += Time.deltaTime;
			index++;
			yield return new WaitForSeconds(intervalTime);
		}
		//faz o personagem voltar a cor normal!
		mat.color = Color.white;
	}

	//Morre
	public void Die()
	{
		morreuMenu.AtivaMenuMorreu ();
		//recarrega a scena
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//Toma dano
	public void Damage(int dmg)
	{
		SoundFXController.PlaySound (soundFx.DANO_PERSONAGEM);
		
		int tiraDano = (dmg - (defesa / 2));
		//Debug.Log("Cor do personagem antes!"+mat.color);

		StartCoroutine (Flash(0.5f, 0.05f));

		rb2D.AddForce (new Vector2 (140,10));

		currentHealth -= dmg;
	}

	public void RecuperaVida(int ganho)
	{
		currentHealth += ganho;
	}

	public void RecuperaMana(int ganho)
	{
		currentMana += ganho;
	}
}


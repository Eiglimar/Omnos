using UnityEngine;
using System.Collections;

public class Caveira2 : MonoBehaviour {

	public LayerMask enemyMask;
	public Rigidbody2D myBody;
	public Transform myTrans;
	public float myWidth;
	public float myHeight;
	public float speed;
	public bool idle;

	//Life
	public int currentHealth;
	public int maxHealth;

	//damage
	public int dano;


	//Patrol
	public float distance;
	public float wakeRange;
	public float attackRange;

	//Projeteis
	/**/
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;
	public GameObject bullet;
	public Transform shootPoint;

	public bool awake = false;
	public bool walk = false;
	public bool physicalAttack = false;
	public bool facingRight = false;
	public bool morreu = false;

	public Transform target;
	public Animator anim;


	//piscar quando toma dano
	//private Material mat;
	//private Color[] colors = { Color.yellow, Color.red };


	// Use this for initialization
	void Start () {
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D> ();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer> ();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
		anim = gameObject.GetComponent<Animator> ();
		idle = true;

		//life
		currentHealth = maxHealth;
	}

	// Update is called once per frame
	void Update () 
	{
		//animacao para morrer
		//anim.SetBool ("morreu",morreu);

		//animacao para andar
		//anim.SetBool ("walk", walk);

		//animacao para idle
		anim.SetBool ("idle", idle);

		//animacao para ataque fisico
		anim.SetBool ("attack", physicalAttack);

		//se tiver sem vida, morre
		if(currentHealth <= 0)
		{
			Die ();
		}

		//se cair morreu
		if(transform.position.y <= -13.0f)
		{
			Die ();
		}
	}

	void Awake()
	{
		//Se o cara tiver a direita ele vira pra direita
		if(target.transform.position.x > myTrans.position.x)
		{
			if(!facingRight)
			{
				flip ();
			}
		}

		//se o cara tiver na esquerda ele vira pra esquerda
		if(target.transform.position.x < myTrans.position.x)
		{
			if(facingRight)
			{
				flip ();
			}
		}
		//movimenta ();
	}

	public void FixedUpdate()
	{
		//verificando a distancia do player com o inimigo
		RangeCheck ();

		//se ele ficar próximo a ponto de ser notado ele vai comecar a seguir o player
		if (awake) {
			Awake ();
		}
		else if(physicalAttack)
		{
			//walk = false;
			idle = false;
		}
		else 
		{	
			idle = true;
			//walk = true;
			Patrulhando ();
		}
	}

	//metodo que faz andar
	void Patrulhando()
	{
		//Checa para ver se tem a layer ground enquanto estiver andando e ver antes de chegar no final de algum lugar.
		Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth / 2 + Vector2.up * myHeight;

		//desenha a linha para eu saber aonde ela esta
		Debug.DrawLine (lineCastPos, lineCastPos + Vector2.down * 2.5f);
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down * 2.5f, enemyMask);
		//desenha a linha para eu saber aonde ela esta
		Debug.DrawLine (lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f);
		bool isBlocked = Physics2D.Linecast (lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);

		// se nao tiver ground de a volta
		if(!isGrounded)
		{
			flip ();
		}

		//movimenta ();
	}

	/*void movimenta()
	{
		//Always move forward
		Vector2 myVel = myBody.velocity;
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;
	}*/

	//método que faz o flip para aonde o inimigo ta olhando
	void flip()
	{
		//só controla o boolean para saber se esta virado para qual lado.
		if (facingRight) 
		{
			facingRight = false;
		} 
		else 
		{
			facingRight = true;	
		}

		//aqui é que gira mesmo!
		Vector3 currRot = myTrans.eulerAngles;
		currRot.y += 180;
		myTrans.eulerAngles = currRot;
	}

	//metodo de verificacao de distancias
	void RangeCheck()
	{
		// Verifica a distancia
		distance = Vector3.Distance (myTrans.position, target.transform.position);

		//mostra a distancia
		//Debug.Log("Distancia: "+distance);

		//comeca a atacar
		if(distance < attackRange)
		{
			physicalAttack = true;
		}
		//para o ataque
		if(distance > attackRange)
		{
			physicalAttack = false;
		}

		//começa a seguir
		if(distance < wakeRange)
		{
			awake = true;
		}
		//para de seguir
		if(distance > wakeRange)
		{
			awake = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(physicalAttack && morreu == false)
			{
				MagicAttack();
				//col.gameObject.GetComponent<Personagem> ().Damage (dano);			
			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			if(physicalAttack && morreu == false)
			{
				MagicAttack();
				//col.gameObject.GetComponent<Personagem> ().Damage (dano);			
			}
		}
	}

	public void MagicAttack()
	{
		bulletTimer += Time.deltaTime;

		if(bulletTimer >= shootInterval)
		{
			SoundFXController.PlaySound (soundFx.CAVEIRA_MAGICA_ATAQUE);
			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize ();

			GameObject bulletClone;
			bulletClone = Instantiate (bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
			bulletTimer = 0;
		}
	}

	public void Damage(int dano)
	{
		//myBody.AddForce (new Vector2(190, 80));
		//StartCoroutine (Flash (0.5f, 0.05f));
		SoundFXController.PlaySound(soundFx.CAVEIRA_DAMAGE);
		currentHealth -= dano;
	}

	IEnumerator SumirObjeto()
	{
		yield return new WaitForSeconds (0.5f);
		Destroy(this.gameObject);
	}

	void Die()
	{
		//seta a variavel que morreu!
		morreu = true;
		//reseta todas as variaveis
		awake = false;
		physicalAttack = false;
		facingRight = false;
		StartCoroutine ("SumirObjeto");
	}
}

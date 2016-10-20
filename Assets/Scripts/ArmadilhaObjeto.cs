using UnityEngine;
using System.Collections;

public class ArmadilhaObjeto : MonoBehaviour {

	public int dano;
	public bool ativada;
	public Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		ativada = false;
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.isKinematic = false;
		rb2d.gravityScale = 3;
		StartCoroutine ("Destruir");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.collider.CompareTag("Player"))
		{
			col.collider.SendMessageUpwards ("Damage", dano);
			rb2d.velocity = new Vector2 (-6, -5);// se eu deixar o y em 0 o personagem não vai pular
			StartCoroutine ("Destruir");
		}
	}

	IEnumerator Manso()
	{
		yield return new WaitForSeconds (2.0f);
		GetComponent<Collider2D> ().enabled = false;
	}

	IEnumerator Destruir()
	{
		yield return new WaitForSeconds (8.0f);
		Destroy (this.gameObject);
	}
}

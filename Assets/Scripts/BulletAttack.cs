using UnityEngine;
using System.Collections;

public class BulletAttack : MonoBehaviour {

	public int dano;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Destruir");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			col.gameObject.GetComponent<Personagem> ().Damage (dano);
			Destroy (this.gameObject);
		}
	}

	IEnumerator Destruir()
	{
		yield return new WaitForSeconds (6.0f);
	}
}

using UnityEngine;
using System.Collections;

public class ArmadilhaFalling : MonoBehaviour {

	public GameObject objeto; //definir no inspector
	public bool ativada;
	public GameObject objectPoint;

	// Use this for initialization
	void Start () {
		ativada = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ativada) {
			GetComponent<Collider2D> ().enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Player"))
		{
			Instantiate(objeto,objectPoint.transform.position, objectPoint.transform.rotation);
			objeto.GetComponent<ArmadilhaObjeto> ().ativada = true;
			ativada = true;
		}
	}
}

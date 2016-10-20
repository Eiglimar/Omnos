using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	private Transform playerPosition;
	private float x,y;
	public float transition;
	public bool useLerp;
	public bool segueY;
	public float ajusteY;

	//variaveis de limites da camera
	public Transform lL;
	public Transform lR;

	// Use this for initialization
	void Start () {
		playerPosition = GameObject.Find("Jogador").transform;
		lL = GameObject.Find ("LL").transform;
		lR = GameObject.Find ("LR").transform;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() //estou usando o fixedUpdate porq a performace fica melhor com o lerp mas poderia ser no LateUpdate
	{
		
	}

	void LateUpdate() // poderia usar esse aqui apra fazer os ajustes da camera mas aqui no meu pc fica lento.
	{
		//transform.position = playerPosition;
		x = playerPosition.position.x; //pega o valor do x para fazer com que a camera siga pelo x do personagem


		if(segueY)
		{
			y = playerPosition.position.y + ajusteY;
		}
		else
		{
			y = transform.position.y;
		}

		if (x > lL.position.x && x < lR.position.x) {

			if (useLerp)
			{
				transform.position = Vector3.Lerp (transform.position, new Vector3 (x, y, transform.position.z), transition);
			} 
			else 
			{
				transform.position = new Vector3 (x, y, transform.position.z);
			}
		}
	}
}

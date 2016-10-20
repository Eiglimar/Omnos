using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteracaoDialog : MonoBehaviour {

	public string textoInteracao;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public string Interacao()
	{
		textoInteracao = textoInteracao.Replace("nn", "\n");
		return textoInteracao;
	}
}

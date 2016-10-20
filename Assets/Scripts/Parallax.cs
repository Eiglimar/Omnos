using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform background; // fundo texture
	public float parallaxScale;
	public float smoothing;
	public Transform cam; //camera
	public Vector3 previousCamPos;


	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;
	}

	// Update is called once per frame
	void Update () {

		//daonde estava menos daonde está vezes a scala do paralax
		float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;
		//o alvo vai recever os novos valores
		float bgTargetX = background.position.x + parallax; 
		Vector3 bgPos = new Vector3(bgTargetX,background.position.y,background.position.z);
		background.position = Vector3.Lerp (background.position, bgPos, smoothing * Time.deltaTime);
		previousCamPos = cam.position;
	}
}

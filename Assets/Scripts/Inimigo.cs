using UnityEngine;
using System.Collections;

public class Inimigo : MonoBehaviour {

	public LayerMask enemyMask;
	public Rigidbody2D myBody;
	public Transform myTrans;
	public float myWidth;
	public float myHeight;
	public float speed;

	// Use this for initialization
	void Start () {
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D> ();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer> ();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
	}

	// Update is called once per frame
	void Update () {

	}

	public void FixedUpdate()
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
			Vector3 currRot = myTrans.eulerAngles;
			currRot.y += 180;
			myTrans.eulerAngles = currRot;
		}

		//Always move forward
		Vector2 myVel = myBody.velocity;
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;
	}
}

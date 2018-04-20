using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
	[SerializeField]	protected float speed;
	[SerializeField]	protected Sprite[] sprites;
	protected Rigidbody2D rb;
	protected bool isGoing = false;
	void Awake(){
		Destroy (gameObject, 10);
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
		rb = GetComponent<Rigidbody2D> ();
	}
	public void go(Vector3 speed){
		rb.velocity = speed;
		isGoing = true;
	} 
	public void stop(){
		GetComponent<Collider2D> ().enabled = false;
		rb.velocity = Vector2.zero;
		isGoing = false;
	}

	void Update () {
		if (isGoing) {
			Vector3 dir = rb.velocity.normalized;
			dir += transform.forward*2;
			rb.velocity = dir * speed;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
	[SerializeField]	protected float speed;
	[SerializeField]	protected Sprite[] sprites;
	protected bool isGoing = false;
	void Awake(){
		Destroy (gameObject, 10);
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
	}
	public void go(Vector3 speed){
		GetComponent<Rigidbody2D> ().velocity = speed;
		isGoing = true;
	} 
	public void stop(){
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		isGoing = false;
	}

	void Update () {
		if (isGoing) {
			Vector3 dir = GetComponent<Rigidbody2D> ().velocity.normalized;
			dir += transform.forward*2;
			GetComponent<Rigidbody2D> ().velocity = dir * speed;
		}
	}
}

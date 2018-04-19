using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {
	protected Vector3 walkTarget;
	protected int state; //0: stay, 1: free walk, 2: follow
	protected float speed = 2f;
	protected Player player;
	protected Rigidbody2D rb;
	protected float time;
	[SerializeField]	protected Blood bloodP;
	[SerializeField]	protected Sprite injured;
	[SerializeField]	protected Vector3 readyPoint;
	[SerializeField]	protected Vector3 finalPoint;
	[SerializeField]	protected Dagger[] dagger;

	public int getState(){
		return state;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		walkTo (readyPoint);
	}

	protected bool walkOneStep(){
		Vector3 dir = walkTarget - transform.position;
		float dist = dir.magnitude;
		dir.Normalize ();
		if (Time.deltaTime * speed < dist) {
			transform.position += speed * dir * Time.deltaTime;
			return false;
		} else {
			if (transform.position != walkTarget) {
				transform.position = walkTarget;
			}
			return true;
		}
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case 0:
			if (walkOneStep ()) {
				state = 1;
				time = 0;
				dagger [0].spin ();
				dagger [1].spin ();
			}
			break;
		case 1:
			if (time > 2f) {
				walkTo (finalPoint);
				state = 3;
				time = 0;
			}
			break;

		case 3:
			if (walkOneStep ()) {
				Destroy (gameObject);
			}
			break;

		case 4:
			Blood blood = Instantiate (bloodP);
			Vector3 unit = Random.onUnitSphere;
			blood.transform.position = transform.position + new Vector3(unit.x, unit.y, 0) * 0.4f;
			blood.setSize (0.5f*Mathf.Pow(Random.Range(0.2f, 1f), 2));

			if (!GetComponent<SpriteRenderer> ().isVisible) {
				Destroy (gameObject);
				return;
			}
			break;
		}
		time += Time.deltaTime;
	}

	public void walkTo(Vector3 walkTarget){
		this.walkTarget = walkTarget;
	}

	public void follow(Player player){
		state = 2;
		rb.isKinematic = false;
		this.player = player;
	}
	public void unfollow(){
		state = 3;
		rb.isKinematic = true;
		rb.velocity = Vector2.zero;
		this.player = null;
		walkTo (finalPoint);
	}
		
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Car")) {
			GetComponent<Collider2D> ().enabled = false;
			if (player) {
				player.letGo ();
			}
			GetComponent<SpriteRenderer> ().sprite = injured;
			Blood blood = Instantiate (bloodP);
			blood.transform.position = transform.position;
			blood.setSize (2f);
			GetComponent<AudioSource> ().Play();

			rb.drag = 0;
			rb.velocity = (transform.position-collision.transform.position).normalized*speed*4f;
			rb.AddTorque (15f);
			state = 4;
		}
	}
}

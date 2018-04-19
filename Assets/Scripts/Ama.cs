using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ama : MonoBehaviour {
	protected Vector3 walkTarget;
	protected int state; //0: stay, 1: free walk, 2: follow
	protected float speed = 2f;
	protected Player player;
	protected Rigidbody2D rb;
	[SerializeField]	protected Blood bloodP;
	[SerializeField]	protected Sprite injured;


	public int getState(){
		return state;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case 1:
			Vector3 dir = walkTarget - transform.position;
			float dist = dir.magnitude;
			dir.Normalize ();
			if (Time.deltaTime * speed < dist) {
				transform.position += speed * dir * Time.deltaTime;
			} else {
				if (transform.position != walkTarget) {
					transform.position = walkTarget;
				}
			}
			break;
		case 3:
			if (!GetComponent<SpriteRenderer> ().isVisible) {
				Destroy (gameObject);
				return;
			}
			dir = new Vector3(-Screen.width*1.5f, Screen.height*1.5f, 0)- transform.position;
			dist = dir.magnitude;
			dir.Normalize ();
			if (Time.deltaTime * speed < dist) {
				transform.position += speed * dir * Time.deltaTime;
			} else {
				if (transform.position != walkTarget) {
					transform.position = walkTarget;
				}
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
	}

	public void walkTo(Vector3 walkTarget){
		state = 1;
		this.walkTarget = walkTarget;
	}
	public bool isQueueing(){
		return state == 0 || state == 1;
	}
	public void follow(Player player){
		state = 2;
		this.player = player;
	}

	protected void die(){
		state = 4;
		GetComponent<Collider2D> ().enabled = false;
		if (player) {
			player.letGo ();
		}
		Player.instance.addCharity (-0.05f);
		//injured, scream and spawn blood
		GetComponent<SpriteRenderer> ().sprite = injured;
		GetComponent<SpriteRenderer> ().sortingOrder = -5;
		GetComponent<AudioSource> ().Play();
		Blood blood = Instantiate (bloodP);
		blood.transform.position = transform.position;
		blood.setSize (2f);
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "Goal") {
			if (state == 3) {
				return;
			} else {
				state = 3;
				GetComponent<Collider2D> ().enabled = false;
				if (player != null) {
					player.letGo ();
				}
				Player.instance.addCharity (0.1f);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Car") || collision.gameObject.layer == LayerMask.NameToLayer ("Killer")) {
			die();
			rb.drag = 0;
			rb.velocity = (transform.position-collision.transform.position).normalized*speed*4f;
			rb.AddTorque (15f);
		}
	}
}

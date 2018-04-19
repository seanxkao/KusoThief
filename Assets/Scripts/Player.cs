using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public static Player instance;
	protected float speed;
	protected int vx;
	protected int vy;
	protected Rigidbody2D rb;
	protected SpringJoint2D joint;

	protected float charity;
	protected float maxCharity = 1f;

	public float getCharity(){
		return charity;
	}
	public float getMaxCharity(){
		return maxCharity;
	}
	public void addCharity(float add){
		charity = Mathf.Clamp(charity+add, 0, maxCharity);
	}

	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
			return;
		}
		instance = this;
		speed = 20f;
		rb = GetComponent<Rigidbody2D> ();
	}

	public void letGo(){
		if(joint!=null){
			Destroy (joint);
		}
	}

	void calculateSpeed(){

		vx = 0;
		vy = 0;
		if (Input.GetKey(KeyCode.LeftArrow)) {
			vx--;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			vx++;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			vy++;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			vy--;
		}
		rb.velocity = (speed * (Vector2.right *vx + Vector2.up * vy));

	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Z)){
			if (joint == null) {
				int mask = LayerMask.GetMask ("Killer");
				Collider2D collider = Physics2D.OverlapBox (transform.position, GetComponent<Collider2D> ().bounds.size * 3.5f, 0, mask);
				if (collider != null) {
					Killer killer = collider.GetComponent<Killer> ();
					if (killer.getState () != 4) {
						killer.follow (this);
						joint = gameObject.AddComponent<SpringJoint2D> ();
						joint.connectedBody = collider.attachedRigidbody;
						joint.autoConfigureDistance = false;
						joint.distance = 0.4f;
						joint.enableCollision = true;
					}
				} else {
					mask = LayerMask.GetMask ("Ama");
					collider = Physics2D.OverlapBox (transform.position, GetComponent<Collider2D> ().bounds.size * 2, 0, mask);
					if (collider != null) {
						Ama ama = collider.GetComponent<Ama> ();
						Debug.Log (ama.getState ());
						if (ama.getState () != 3) {
							ama.follow (this);
							joint = gameObject.AddComponent<SpringJoint2D> ();
							joint.connectedBody = collider.attachedRigidbody;
							joint.autoConfigureDistance = false;
							joint.distance = 0.2f;
							joint.enableCollision = true;
						}
					}
				}

			} else {
				Killer killer = joint.connectedBody.GetComponent<Killer> ();
				if (killer) {
					killer.unfollow ();
				}
				Destroy (joint);
			}
		}
		if (joint == null) {
			speed = 4f;
		} else {
			speed = 3f;
		}

		calculateSpeed ();
	}



}

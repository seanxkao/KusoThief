using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {
	int state;
	float maxSize = 1;
	float time;
	float stayDuration = 20f;
	float disappearDuration = 3f;

	// Use this for initialization
	void Start () {
		transform.localEulerAngles = Random.Range(0, 360) * Vector3.forward;
		transform.localScale = Vector3.one * maxSize;
	}

	public void setSize(float size){
		maxSize = size;
		transform.localScale = Vector3.one * size;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case 0:
			if (time > stayDuration) {
				time = 0;
				state = 1;
			}
			break;
		case 1:
			Color color = new Color (1, 1, 1, 1-time / disappearDuration);
			GetComponent<SpriteRenderer> ().color = color;
			if (time > disappearDuration) {
				Destroy (gameObject);
			}
			break;
		}
		time += Time.deltaTime;
	}
}

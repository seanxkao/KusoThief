using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour {
	int state;
	float inDuration = 0.5f;
	float stayDuration = 1f;
	float outDuration = 0.5f;
	float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case 0:
			if (time > inDuration) {
				time = 0;
				state = 1;
			}			
			break;
		case 1:
			if (time > stayDuration) {
				time = 0;
				state = 2;
			}			
			break;
		case 2:
			if (time > outDuration) {
				Destroy (gameObject);
			}			
			break;
		}
		time += Time.deltaTime;
	}
}

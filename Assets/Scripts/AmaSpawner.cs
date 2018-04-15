using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaSpawner : MonoBehaviour {
	[SerializeField]	protected Ama amaP;
	[SerializeField]	protected float duration;
	protected float lastSpawn = -1000;

	void Update(){
		float now = Time.time;
		if (now > lastSpawn + duration) {
			lastSpawn = now;
			if (!AmaQueue.instance.isFull ()) {
				Ama ama = Instantiate (amaP);
				amaP.transform.position = transform.position;
				AmaQueue.instance.addAma(ama);
			}
		}
	}
}

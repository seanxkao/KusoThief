using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSpawner : MonoBehaviour {
	[SerializeField]	protected Killer killerP;
	[SerializeField]	protected float nextSpawn;
	[SerializeField]	protected float minDuration;
	[SerializeField]	protected float maxDuration;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			Killer killer = Instantiate (killerP);
			killer.transform.position = transform.position;
			nextSpawn = Time.time + Random.Range (minDuration, maxDuration);
		}
	}
}

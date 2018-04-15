using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
	[SerializeField]	protected Vector3 dir;
	[SerializeField]	protected Car carP;
	[SerializeField]	protected float nextSpawn;
	[SerializeField]	protected float minDuration;
	[SerializeField]	protected float maxDuration;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn) {
			Car car = Instantiate (carP);
			car.transform.position = transform.position;
			nextSpawn = Time.time + Random.Range (minDuration, maxDuration);
			car.go (dir);
		}
	}
}

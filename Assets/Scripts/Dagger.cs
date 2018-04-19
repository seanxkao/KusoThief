using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour {
	bool isSpinning = false;
	float time;
	[SerializeField]	protected float speed;

	public void spin(){
		isSpinning = true;
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSpinning) {
			Vector3 angle = transform.localEulerAngles;
			angle.z += Mathf.Clamp (time*speed/2, -Mathf.Abs(speed), Mathf.Abs(speed));
			transform.localEulerAngles = angle;
			time += Time.deltaTime;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : Bar {

	
	protected bool counting = false;

	protected override void Start(){
		base.Start();
		max = 10f;
		value = max;
	}

	public void countDown(){
		counting = true;
	}

	protected override void updateValue()
	{
		//do nothing
	}
	void Update()
	{
		if (counting) {
			value -= Time.deltaTime;
			value = Mathf.Clamp (value, 0, max);
		}
	}

}

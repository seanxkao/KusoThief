using UnityEngine;
using System.Collections;

public class HpBar : Bar {
	Player player;

	protected override void Start(){
		base.Start();
		player = target.GetComponent<Player>();
	}

    protected override void updateValue()
    {
		value = player.getCharity();
		max = player.getMaxCharity();
    }
	public float getCharity(){
		return value;
	}
	public bool isKillerActivate(){
		return value / max >= 0.39f;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class SetMaxCharity : ActionNode {
	public override void Start () {
		base.Start ();
		this.blackboard.GetFloatVar ("MaxCharity").Value = GameObject.FindObjectOfType<CharityPasser> ().charity;
	}

	public override Status Update () {
		return Status.Success;
	}
}

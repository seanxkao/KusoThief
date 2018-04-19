using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class InitVar : ActionNode {
	public override void Start () {
		base.Start ();
        CharityPasser passer = GameObject.FindObjectOfType<CharityPasser> ();
        if (passer != null) {
            this.blackboard.GetFloatVar ("MaxCharity").Value = passer.charity;
        }
        else {
            this.blackboard.GetFloatVar ("MaxCharity").Value = 1;
        }

        Requester requester = GameObject.FindObjectOfType<Requester> ();
        if (requester != null) {
            this.blackboard.GetIntVar ("TargetID").Value = requester.requestID;
            this.blackboard.GetObjectVar("TargetImage").Value = requester.getTargetImage();
        }
        else {
            this.blackboard.GetIntVar ("TargetID").Value = 1;
        }
	}

	public override Status Update () {
		return Status.Success;
	}
}

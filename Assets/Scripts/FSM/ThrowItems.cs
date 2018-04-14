using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class ThrowItems : ActionNode {

    public GameObjectVar target;
    public Vector3Var throwPosition;

	public override void Start () {
        base.Start (); 
        target.Value.GetComponent<Target>().emit(throwPosition.Value);
    }

    public override Status Update () {
        return Status.Success;
    }
}

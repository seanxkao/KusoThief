using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class ThrowItems : ActionNode {

    public GameObjectVar target;
    public GameObjectVar player;

	public override void Start () {
        base.Start (); 
        target.Value.GetComponent<Target>().emit(player.Value.transform);
    }

    public override Status Update () {
        return Status.Success;
    }
}

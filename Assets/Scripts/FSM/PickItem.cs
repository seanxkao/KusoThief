using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class PickItem : ActionNode {

    public GameObjectVar item;
    public GameObjectVar player;

    public override void Start () {
        base.Start (); 
        item.transform.parent = player.transform;
    }

    public override Status Update () {
        return Status.Success;
    }
}

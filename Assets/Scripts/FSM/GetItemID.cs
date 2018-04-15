using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class GetItemID : ActionNode {

    public GameObjectVar item;
    public IntVar ID;

    public override void Start () {
        base.Start (); 
        ID.Value = item.Value.GetComponent<Item> ().ID;
    }

    public override Status Update () {
        return Status.Success;
    }
}

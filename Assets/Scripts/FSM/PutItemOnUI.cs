using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviourMachine;

[NodeInfo(category = "Action/Thief/")]
public class PutItemOnUI : ActionNode {
    public GameObjectVar item;
    public GameObjectVar UI;

    public override void Start () {
        base.Start ();
        UI.Value.GetComponent<Image> ().sprite = item.Value.GetComponent<Item> ().getImage ();
    }
    public override Status Update () {
        return Status.Success;
    }
}

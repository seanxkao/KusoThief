using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviourMachine;

[NodeInfo(category = "UI/")]
public class SetItemImage : ActionNode {

    public GameObjectVar item;
    public GameObjectVar image;

    public override void Start () {
        base.Start (); 
        image.Value.GetComponent<Image> ().sprite = item.Value.GetComponent<Item> ().getImage ();
    }

    public override Status Update () {
        return Status.Success;
    }
}

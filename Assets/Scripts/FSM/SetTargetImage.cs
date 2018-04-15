using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviourMachine;

[NodeInfo(category = "UI/")]
public class SetTargetImage : ActionNode {

    public ObjectVar target;
    public GameObjectVar image;

    public override void Start () {
        base.Start (); 
        image.Value.GetComponent<Image> ().sprite = (Sprite) target.Value;
    }

    public override Status Update () {
        return Status.Success;
    }
}

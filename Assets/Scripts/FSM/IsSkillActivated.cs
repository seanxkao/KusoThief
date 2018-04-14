using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

namespace Compass.FSM {

    [NodeInfo(category = "Condition/Thief/")]
    public class IsSkillActivated : ConditionNode {

        private float prvAxis;
        private float prvTriggerTime;
        private Status nowStatus;
        public FloatVar timeout;

        public override void Awake () {
            base.Awake();
            Reset();
        }
        public override void Reset () {
            base.Reset ();
            prvAxis = 0;
            prvTriggerTime = Time.time;
            nowStatus = Status.Failure;
        }
        public override void OnEnable () {
            base.OnEnable ();
            //Reset ();
        }
        public override Status Update () {

            if (Time.time - prvTriggerTime > timeout.Value) {
                Reset ();
            }
            else {
                float axis = Input.GetAxis ("Vertical");
                if (axis != 0) {
                    if (axis * prvAxis < 0) {               
                        nowStatus = Status.Success;
                    }
                    prvTriggerTime = Time.time;
                    prvAxis = axis;
                }
            }

            return Status.Success;
        }
    }
}

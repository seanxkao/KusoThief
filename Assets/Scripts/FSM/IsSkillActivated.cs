using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

namespace Compass.FSM {

    [NodeInfo(category = "Condition/Thief/")]
    public class IsSkillActivated : ConditionNode {

        private float prvAxis;
        private float prvTriggerTime;
        private float timeout = 0.5f;

        public override void Reset () {
            base.Reset ();
            prvAxis = 0;
            prvTriggerTime = Time.time;
        }
        public override void OnEnable () {
            base.OnEnable ();
            Reset ();
        }
        public override Status Update () {
            Status status = Status.Failure;
            if (Time.time - prvTriggerTime > timeout) {
                Reset ();
            }
            else {
                float axis = Input.GetAxis ("Vertical");
                if (axis != 0) {
                    if (axis * prvAxis < 0) {
                        prvTriggerTime = Time.time;
                        status = Status.Success;
                    }
                    prvAxis = axis;
                }
            }

            return status;
        }
    }
}

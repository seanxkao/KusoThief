using UnityEngine;
using System.Collections;

namespace BehaviourMachine
{

    /// <summary>
    /// Moves the "Game Object" to the desired position using the transform.
    /// </summary>
    [NodeInfo(category = "Action/Tween/",
                icon = "Tween",
                description = "Moves the \"Game Object\" to the desired position using the transform")]
    public class MoveTo : TweenGameObjectNode
    {

        /// <summary>
        /// The type of easing.
        /// </summary>
        [Tooltip("The type of easing")]
        public EaseType easeType = TweenNode.EaseType.easeInQuad;

        /// <summary>
        /// The desired position.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Use Self", tooltip = "The desired position")]
        public GameObjectVar desiredPosition;

        [VariableInfo(requiredField = false, nullLabel = "Don't Ignore", tooltip = "If set to True ignores the x axis")]
        public BoolVar ignoreXAxis;

        /// <summary>
        /// If set to True ignores the y axis.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Ignore", tooltip = "If set to True ignores the y axis")]
        public BoolVar ignoreYAxis;

        /// <summary>
        /// If set to True ignores the z axis.
        /// </summary>
        [VariableInfo(requiredField = false, nullLabel = "Don't Ignore", tooltip = "If set to True ignores the z axis")]
        public BoolVar ignoreZAxis;

        public override void Reset()
        {
            base.Reset();
            easeType = TweenNode.EaseType.easeInQuad;
            desiredPosition = new ConcreteGameObjectVar(this.self);
            ignoreXAxis = new ConcreteBoolVar();
            ignoreYAxis = new ConcreteBoolVar();
            ignoreZAxis = new ConcreteBoolVar();
        }

        public override void OnValidate()
        {
            UpdateEasingFunction(easeType);
        }

        public override void OnStart()
        {
            Vector3 from = gameObject.Value != null ? gameObject.Value.transform.position : Vector3.zero;
            Vector3 to = desiredPosition.Value != null ? desiredPosition.Value.transform.position : Vector3.zero;

            m_From = new float[] { from.x, from.y, from.z };
            m_To = new float[] { to.x, to.y, to.z };
            m_Result = new float[3];


            if (!ignoreXAxis.isNone && ignoreXAxis.Value) m_To[0] = from.x;
            if (!ignoreYAxis.isNone && ignoreYAxis.Value) m_To[1] = from.y;
            if (!ignoreZAxis.isNone && ignoreZAxis.Value) m_To[2] = from.z;
        }

        public override void OnFinish()
        {
            transform.position = desiredPosition.Value != null ? desiredPosition.Value.transform.position : Vector3.zero;
        }

        public override void OnUpdate()
        {
            // Update ease function?
            if (m_EaseFunction == null)
                UpdateEasingFunction(easeType);

            Apply();

            transform.position = new Vector3(m_Result[0], m_Result[1], m_Result[2]);
        }
    }
}
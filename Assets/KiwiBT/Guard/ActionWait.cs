using KiwiBehaviorTree;
using UnityEngine;

namespace Assets
{
    public class ActionWait : Action
    {
        private float _duration = 1f;
        private float _endTime = 0f;

        public ActionWait(float delay = 1f)
        {
            _duration = delay;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _endTime = Time.time + _duration;
        }

        public override NodeState OnUpdate()
        {
            if (Time.time >= _endTime)
            {
                return NodeState.Success;
            }
            return NodeState.Running;
        }
    }
}

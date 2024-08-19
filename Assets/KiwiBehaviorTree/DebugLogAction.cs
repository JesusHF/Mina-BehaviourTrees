using UnityEngine;

namespace KiwiBehaviorTree
{
    public class DebugLogAction : Action
    {
        public string _message;

        public DebugLogAction(string message)
        {
            _message = message;
        }

        public override void OnEnter()
        {
            Debug.Log($"OnEnter {_message}");
        }

        public override NodeState OnUpdate()
        {
            Debug.Log($"OnUpdate {_message}");
            return NodeState.Success;
        }

        public override void OnExit()
        {
            Debug.Log($"OnExit {_message}");
        }
    }
}
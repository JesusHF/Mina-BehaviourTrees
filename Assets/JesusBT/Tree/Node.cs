namespace Jesushf
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public abstract class Node
    {
        protected Node _parent;
        protected NodeState _state = NodeState.Running;
        private bool _isStarted = false;

        public void SetParent(Node parent) { _parent = parent; }

        public NodeState Evaluate()
        {
            if (!_isStarted)
            {
                OnEnter();
                _isStarted = true;
            }

            NodeState state = OnUpdate();

            if (state != NodeState.Running && _isStarted)
            {
                OnExit();
                _isStarted = false;
            }

            return state;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        public virtual NodeState OnUpdate()
        {
            return NodeState.Success;
        }
    }

    public abstract class Action : Node { }
    public abstract class Condition : Node { }
}

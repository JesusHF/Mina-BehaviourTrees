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
        private bool _isFinished = false;

        public void SetParent(Node parent) { _parent = parent; }
        public virtual void Restart()
        {
            _isStarted = false;
            _isFinished = false;
        }

        public NodeState Evaluate()
        {
            if (_isStarted && _isFinished)
            {
                return _state;
            }

            if (!_isStarted)
            {
                OnEnter();
                _isStarted = true;
            }

            NodeState state = OnUpdate();

            if (state != NodeState.Running && !_isFinished)
            {
                OnExit();
                _isFinished = true;
            }

            _state = state;
            return _state;
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

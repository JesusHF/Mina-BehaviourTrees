namespace Jesushf
{
    public enum NodeStatus
    {
        Running,
        Success,
        Failure
    }

    public abstract class Node
    {
        protected Node _parent;
        protected NodeStatus _state = NodeStatus.Running;
        private bool _isStarted = false;
        private bool _isFinished = false;

        public void SetParent(Node parent) { _parent = parent; }
        public virtual void Restart()
        {
            _isStarted = false;
            _isFinished = false;
        }

        public NodeStatus Evaluate()
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

            NodeStatus state = OnUpdate();

            if (state != NodeStatus.Running && !_isFinished)
            {
                OnExit();
                _isFinished = true;
            }

            _state = state;
            return _state;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        public virtual NodeStatus OnUpdate()
        {
            return NodeStatus.Success;
        }
    }

    public abstract class Action : Node { }
    public abstract class Condition : Node { }
}

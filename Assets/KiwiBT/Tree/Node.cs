using System.Collections.Generic;

namespace KiwiBehaviorTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public abstract class Node
    {
        protected NodeState _state;
        protected Node _parent;
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        private bool _isStarted = false;

        public Node Parent { get { return _parent; } set { _parent = value; } }

        public Node()
        {
            _parent = null;
        }

        public NodeState Update()
        {
            if (!_isStarted)
            {
                OnEnter();
                _isStarted = true;
            }

            _state = OnUpdate();

            if (_state == NodeState.Failure || _state == NodeState.Success)
            {
                OnExit();
                _isStarted = false;
            }

            return _state;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual NodeState OnUpdate()
        {
            return NodeState.Failure;
        }

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            Node node = _parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.Parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = _parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.Parent;
            }
            return false;
        }
    }
}

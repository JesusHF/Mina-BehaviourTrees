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
        public Node Parent { get { return _parent; } set { _parent = value; } }

        public virtual NodeState OnUpdate()
        {
            return NodeState.Success;
        }
    }

    public abstract class Action : Node { }
    public abstract class Condition : Node { }
}

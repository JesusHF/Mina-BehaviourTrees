namespace KiwiBehaviorTree
{
    public abstract class Decorator : Node
    {
        protected Node _child = null;

        public Decorator(Node child)
        {
            child.Parent = this;
            _child = child;
        }
    }
}

namespace Jesushf
{
    public class Decorator : Node
    {
        protected Node _child;

        public Decorator(Node child) : base()
        {
            child.Parent = this;
            _child = child;
        }
    }
}

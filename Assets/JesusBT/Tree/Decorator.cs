namespace Jesushf
{
    public class Decorator : Node
    {
        protected Node _child = null;

        public Decorator(Node child) : base()
        {
            child.SetParent(this);
            _child = child;
        }
    }
}

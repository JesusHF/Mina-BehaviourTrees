using System.Collections.Generic;

namespace Jesushf
{
    public abstract class Composite : Node
    {
        protected List<Node> _children;

        public Composite(List<Node> children) : base()
        {
            foreach (Node child in children)
            {
                child.Parent = this;
                _children.Add(child);
            }
        }
    }
}

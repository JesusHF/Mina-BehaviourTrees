using System.Collections.Generic;

namespace Jesushf
{
    public abstract class Composite : Node
    {
        protected List<Node> _children = new();

        public Composite(List<Node> children) : base()
        {
            foreach (Node child in children)
            {
                child.SetParent(this);
                _children.Add(child);
            }
        }
    }
}

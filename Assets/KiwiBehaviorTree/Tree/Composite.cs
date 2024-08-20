using System.Collections.Generic;

namespace KiwiBehaviorTree
{
    public abstract class Composite : Node
    {
        protected List<Node> _children = new();

        public Composite() : base() { }
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

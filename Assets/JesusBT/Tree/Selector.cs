using System.Collections.Generic;

namespace Jesushf
{
    public class Selector : Composite
    {
        public Selector(List<Node> children) : base(children) { }

        public override NodeStatus OnUpdate()
        {
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeStatus.Running:
                        _state = NodeStatus.Running;
                        return _state;
                    case NodeStatus.Success:
                        _state = NodeStatus.Success;
                        return _state;
                    default: break;
                }
            }

            _state = NodeStatus.Failure;
            return _state;
        }
    }
}

using System.Collections.Generic;

namespace Jesushf
{
    public class Sequence : Composite
    {
        public Sequence(List<Node> children) : base(children) { }

        public override NodeStatus OnUpdate()
        {
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeStatus.Running:
                        _state = NodeStatus.Running;
                        return _state;
                    case NodeStatus.Failure:
                        _state = NodeStatus.Failure;
                        return _state;
                    default: break;
                }
            }

            _state = NodeStatus.Success;
            return _state;
        }
    }
}

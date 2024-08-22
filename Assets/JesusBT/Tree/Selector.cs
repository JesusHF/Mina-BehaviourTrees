using System.Collections.Generic;

namespace Jesushf
{
    public class Selector : Composite
    {
        public Selector(List<Node> children) : base(children) { }

        public override NodeState OnUpdate()
        {
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Running:
                        _state = NodeState.Running;
                        return _state;
                    case NodeState.Success:
                        _state = NodeState.Success;
                        return _state;
                    default: break;
                }
            }

            _state = NodeState.Failure;
            return _state;
        }
    }
}

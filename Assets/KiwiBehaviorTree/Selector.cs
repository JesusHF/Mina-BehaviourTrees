using System.Collections.Generic;

namespace KiwiBehaviorTree
{
    public class Selector : Composite
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState OnUpdate()
        {
            foreach (Node node in _children)
            {
                switch (node.Update())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        _state = NodeState.Success;
                        return _state;
                    case NodeState.Running:
                        _state = NodeState.Running;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = NodeState.Failure;
            return _state;
        }
    }
}

using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in _children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        _state = NodeState.Failure;
                        return _state;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        _state = NodeState.Success;
                        return _state;
                }
            }

            _state = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return _state;
        }
    }
}

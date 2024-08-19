using System.Collections.Generic;

namespace KiwiBehaviorTree
{
    public class Sequence : Composite
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState OnUpdate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in _children)
            {
                switch (node.Update())
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

using UnityEngine;
using UnityEngine.Assertions;

namespace Jesushf
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        private Node _root = null;
        private NodeStatus _state = NodeStatus.Running;

        private void Start()
        {
            _root = SetupTree();
            Assert.IsNotNull(_root);
        }

        private void Update()
        {
            if (_state != NodeStatus.Running)
            {
                _state = NodeStatus.Running;
                _root.Restart();
            }

            _state = _root.OnUpdate();
        }

        protected abstract Node SetupTree();
    }
}

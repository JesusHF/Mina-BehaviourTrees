using UnityEngine;
using UnityEngine.Assertions;

namespace Jesushf
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        private Node _root = null;
        private NodeState _state = NodeState.Running;

        private void Start()
        {
            _root = SetupTree();
            Assert.IsNotNull(_root);
        }

        private void Update()
        {
            _state = _root.OnUpdate();
        }

        protected abstract Node SetupTree();
    }
}

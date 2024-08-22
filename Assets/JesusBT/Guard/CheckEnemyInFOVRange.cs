using UnityEngine;

namespace Jesushf
{
    public class CheckEnemyInFOVRange : Action
    {
        private static int _enemyLayerMask = 1 << 6;

        private Transform _transform;
        private Animator _animator;

        public CheckEnemyInFOVRange(Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeState OnUpdate()
        {
            JesusBTGuard guard = _transform.GetComponent<JesusBTGuard>();
            Transform t = guard.Target;
            if (t == null)
            {
                Collider[] colliders = Physics.OverlapSphere(
                    _transform.position, GuardBT.fovRange, _enemyLayerMask);

                if (colliders.Length > 0)
                {
                    guard.Target = colliders[0].transform;
                    _animator.SetBool("Walking", true);
                    return NodeState.Success;
                }

                return NodeState.Failure;
            }

            return NodeState.Success;
        }
    }
}
using UnityEngine;

namespace Jesushf
{
    public class CheckEnemyInAttackRange : Action
    {
        private Transform _transform;
        private Animator _animator;

        public CheckEnemyInAttackRange(Transform transform)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeStatus OnUpdate()
        {
            JesusBTGuard guard = _transform.GetComponent<JesusBTGuard>();
            Transform target = guard.Target;
            if (target == null)
            {
                return NodeStatus.Failure;
            }

            if (Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange)
            {
                _animator.SetBool("Attacking", true);
                _animator.SetBool("Walking", false);

                return NodeStatus.Success;
            }

            return NodeStatus.Failure;
        }
    }
}
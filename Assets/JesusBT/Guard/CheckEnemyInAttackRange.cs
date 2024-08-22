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

        public override NodeState OnUpdate()
        {
            JesusBTGuard guard = _transform.GetComponent<JesusBTGuard>();
            Transform target = guard.Target;
            if (target == null)
            {
                _state = NodeState.Failure;
                return _state;
            }

            if (Vector3.Distance(_transform.position, target.position) <= GuardBT.attackRange)
            {
                _animator.SetBool("Attacking", true);
                _animator.SetBool("Walking", false);

                _state = NodeState.Success;
                return _state;
            }

            _state = NodeState.Failure;
            return _state;
        }
    }
}
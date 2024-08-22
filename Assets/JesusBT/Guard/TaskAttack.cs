using UnityEngine;

namespace Jesushf
{
    public class TaskAttack : Action
    {
        private Animator _animator;
        private JesusBTGuard guard = null;

        private Transform _lastTarget;
        private EnemyManager _enemyManager;

        private float _attackTime = 1f;
        private float _attackCounter = 0f;

        public TaskAttack(Transform transform)
        {
            _animator = transform.GetComponent<Animator>();
            guard = transform.GetComponent<JesusBTGuard>();
        }

        public override NodeState OnUpdate()
        {
            Transform target = guard.Target;
            if (target != _lastTarget)
            {
                _enemyManager = target.GetComponent<EnemyManager>();
                _lastTarget = target;
            }

            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime)
            {
                bool enemyIsDead = _enemyManager.TakeHit();
                if (enemyIsDead)
                {
                    guard.Target = null;
                    _animator.SetBool("Attacking", false);
                    _animator.SetBool("Walking", true);
                }
                else
                {
                    _attackCounter = 0f;
                }
            }

            _state = NodeState.Running;
            return _state;
        }
    }
}
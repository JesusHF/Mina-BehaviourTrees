using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerTree
{
    [TaskCategory("Knight")]
    public class BDActionAttack : Action
    {
        public SharedTransform Target;
        private Transform _lastTarget = null;
        private Animator _animator = null;
        private EnemyManager _enemyManager = null;
        private float _attackTime = 1f;
        private float _attackCounter = 0f;

        public override void OnStart()
        {
            _animator = transform.GetComponent<Animator>();
            _animator.SetBool("Walking", false);
            _animator.SetBool("Attacking", true);
        }

        public override TaskStatus OnUpdate()
        {
            Transform target = Target.Value;
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
                    return TaskStatus.Success;
                }
                else
                {
                    _attackCounter = 0f;
                }
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            _animator.SetBool("Attacking", false);
        }
    }
}

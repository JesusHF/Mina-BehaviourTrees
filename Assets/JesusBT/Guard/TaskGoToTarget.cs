using UnityEngine;

namespace Jesushf
{
    public class TaskGoToTarget : Action
    {
        private Transform _transform = null;
        private Animator _animator = null;

        public TaskGoToTarget(Transform transform)
        {
            _transform = transform;
        }

        public override void OnEnter()
        {
            _animator = _transform.GetComponent<Animator>();
            _animator.SetBool("Walking", true);
        }

        public override NodeState OnUpdate()
        {
            JesusBTGuard guard = _transform.GetComponent<JesusBTGuard>();
            Transform target = guard.Target;
            if (Vector3.Distance(_transform.position, target.position) <= JesusBTGuard.attackRange)
            {
                return NodeState.Success;
            }

            _transform.position = Vector3.MoveTowards(_transform.position, target.position, JesusBTGuard.speed * Time.deltaTime);
            _transform.LookAt(target.position);
            return NodeState.Running;
        }

        public override void OnExit()
        {
            _animator.SetBool("Walking", false);
        }
    }
}
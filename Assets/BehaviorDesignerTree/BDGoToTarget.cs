using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerTree
{
    [TaskCategory("Knight")]
    public class BDGoToTarget : Action
    {
        public SharedTransform Target;

        private Transform _transform;
        private Animator _animator;

        public override void OnStart()
        {
            _transform = this.transform;
            _animator = transform.GetComponent<Animator>();
            _animator.SetBool("Walking", true);
        }

        public override TaskStatus OnUpdate()
        {
            Transform target = Target.Value;
            if (Vector3.Distance(_transform.position, target.position) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, target.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(target.position);
                return TaskStatus.Running;
            }
            return TaskStatus.Success;
        }

        public override void OnEnd()
        {
            _animator.SetBool("Walking", false);
        }
    }
}

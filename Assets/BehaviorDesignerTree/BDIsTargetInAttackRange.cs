using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerTree
{
    [TaskCategory("Knight")]
    public class BDIsTargetInAttackRange : Conditional
    {
        public SharedTransform Target;
        private Transform _transform;

        public override void OnStart()
        {
            _transform = this.transform;
        }

        public override TaskStatus OnUpdate()
        {
            if (Target.Value == null)
            {
                return TaskStatus.Failure;
            }

            float distance = Vector3.Distance(_transform.position, Target.Value.position);
            if (distance <= GuardBT.attackRange)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}

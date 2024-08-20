using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerTree
{
    [TaskCategory("Knight")]
    public class BDISTargetInSight : Conditional
    {
        public SharedTransform Target;

        private static int _enemyLayerMask = (1 << 6);
        private Transform _transform;

        public override void OnStart()
        {
            _transform = this.transform;
        }

        public override TaskStatus OnUpdate()
        {
            if (Target.Value != null)
            {
                return TaskStatus.Success;
            }

            Collider[] colliders = Physics.OverlapSphere(_transform.position, GuardBT.fovRange, _enemyLayerMask);
            if (colliders.Length > 0)
            {
                Target.Value = colliders[0].transform;
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}

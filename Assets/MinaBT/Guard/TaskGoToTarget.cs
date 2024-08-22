using UnityEngine;
using KiwiBehaviorTree;

public class TaskGoToTarget : Action
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState OnUpdate()
    {
        Transform target = (Transform)GetData("target");
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, GuardBT.speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }

        _state = NodeState.Running;
        return _state;
    }
}

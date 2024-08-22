using UnityEngine;
using UnityEngine.Assertions;

namespace Jesushf
{
    public class TaskPatrol : Action
    {
        private Transform _transform;
        private Animator _animator;

        private int _currentWaypointIndex = 0;
        private Transform[] _waypoints;

        private float _waitTime = 1f; // in seconds
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _waypoints = waypoints;
            Assert.IsNotNull(_transform);
            Assert.IsNotNull(_animator);
        }

        public override void OnEnter()
        {
            _currentWaypointIndex = GetClosestWaypointIndex();
            _waiting = true;
        }

        public override NodeStatus OnUpdate()
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                    _animator.SetBool("Walking", true);
                }
            }
            else
            {
                Transform currentWaypoint = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_transform.position, currentWaypoint.position) < 0.01f)
                {
                    _transform.position = currentWaypoint.position;
                    return NodeStatus.Success;
                }
                else
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, currentWaypoint.position, JesusBTGuard.speed * Time.deltaTime);
                    _transform.LookAt(currentWaypoint.position);
                }
            }

            return NodeStatus.Running;
        }

        public override void OnExit()
        {
            _waitCounter = 0f;
            _waiting = true;
            _animator.SetBool("Walking", false);
        }

        private int GetClosestWaypointIndex()
        {
            int waypointIndex = 0;
            float closestWaypointDistance = Vector3.Distance(_transform.position, _waypoints[waypointIndex].position);
            for (int i = 1; i < _waypoints.Length; i++)
            {
                float currentDistance = Vector3.Distance(_transform.position, _waypoints[i].position);
                if (currentDistance < closestWaypointDistance)
                {
                    waypointIndex = i;
                    closestWaypointDistance = currentDistance;
                }
            }

            if (closestWaypointDistance <= 0.1f)
            {
                waypointIndex = (waypointIndex + 1) % _waypoints.Length;
            }
            return waypointIndex;
        }
    }
}

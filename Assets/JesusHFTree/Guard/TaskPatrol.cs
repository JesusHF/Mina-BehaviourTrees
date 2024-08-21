using UnityEngine;

namespace Jesushf
{
    public class TaskPatrol : Action
    {
        private Transform _transform;
        private Animator _animator;
        private Transform[] _waypoints;

        private int _currentWaypointIndex = 0;

        private float _waitTime = 1f; // in seconds
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public TaskPatrol(Transform transform, Transform[] waypoints)
        {
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _waypoints = waypoints;
        }

        public override NodeState OnUpdate()
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
                Transform waypoint = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_transform.position, waypoint.position) < 0.01f)
                {
                    _transform.position = waypoint.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                    _animator.SetBool("Walking", false);
                }
                else
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, waypoint.position, GuardBT.speed * Time.deltaTime);
                    _transform.LookAt(waypoint.position);
                }
            }

            _state = NodeState.Running;
            return _state;
        }
    }
}

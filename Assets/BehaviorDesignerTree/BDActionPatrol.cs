using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesignerTree
{
    [TaskCategory("Knight")]
    public class BDActionPatrol : Action
    {
        public SharedTransformList GlobalWaypointList;
        private Transform _transform;
        private Animator _animator;

        private int _currentWaypointIndex = 0;
        private List<Transform> _waypoints;

        private float _waitTime = 1f; // in seconds
        private float _waitCounter = 0f;
        private bool _waiting = false;

        public override void OnStart()
        {
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            Assert.IsNotNull(_transform);
            Assert.IsNotNull(_animator);

            Assert.IsNotNull(GlobalWaypointList);
            _waypoints = GlobalWaypointList.Value;
        }

        public override TaskStatus OnUpdate()
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
                    _waitCounter = 0f;
                    _waiting = true;

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
                    _animator.SetBool("Walking", false);
                    return TaskStatus.Success;
                }
                else
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, currentWaypoint.position, GuardBT.speed * Time.deltaTime);
                    _transform.LookAt(currentWaypoint.position);
                }
            }

            return TaskStatus.Running;
        }
    }
}

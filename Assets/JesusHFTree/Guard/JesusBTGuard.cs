using System.Collections.Generic;
using UnityEngine;
using Jesushf;

public class JesusBTGuard : Jesushf.BehaviorTree
{
    public Transform[] waypoints;
    public static float speed { get { return 2f; } }
    public static float fovRange { get { return 6f; } }
    public static float attackRange { get { return 1f; } }

    private Transform _target = null;
    public Transform Target { get => _target; set => _target = value; }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new Jesushf.CheckEnemyInAttackRange(this.transform),
                new Jesushf.TaskAttack(this.transform),
            }),
            new Sequence(new List<Node>
            {
                new Jesushf.CheckEnemyInFOVRange(this.transform),
                new Jesushf.TaskGoToTarget(this.transform),
            }),
            new Jesushf.TaskPatrol(this.transform, waypoints),
        });

        return root;
    }
}

using System.Collections.Generic;
using UnityEngine;
using KiwiBehaviorTree;

public class GuardBT : BehaviorTree
{
    public Transform[] waypoints;
    public static float speed { get { return 2f; } }
    public static float fovRange { get { return 6f; } }
    public static float attackRange { get { return 1f; } }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(this.transform),
                new TaskAttack(this.transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(this.transform),
                new TaskGoToTarget(this.transform),
            }),
            new TaskPatrol(this.transform, waypoints),
        });

        return root;
    }
}

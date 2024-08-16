using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class GuardBT : BehaviorTree.Tree
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
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}

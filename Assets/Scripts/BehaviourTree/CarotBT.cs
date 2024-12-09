using System;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class CarotBT : Tree
{
    public static float speed = 1.5f;
    public static float attackRange = 5f;
    public static float attackCooldown = 3f;
    public static string attackType = "carot";
    private EnemySpriteFlip Flip;

    protected override Node SetupTree()
    {
        Unit unit = GetComponent<Unit>();
        // I added this line bernas
        unit.target = GameObject.FindGameObjectWithTag("Player").transform;
        Flip = new EnemySpriteFlip(transform);

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInAttackRange(transform, attackRange, unit, attackType),
                new TaskCheckCollision(transform),
                new TaskAttack(transform, unit, attackType, attackCooldown),
            }),
            new Sequence(new List<Node>
            {
                new TaskGoToTarget(transform, unit),
            }),

        });

        return root;
    }

    private void FixedUpdate()
    {
        Flip.Update();
    }
}

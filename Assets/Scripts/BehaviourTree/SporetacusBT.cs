using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class SporetacusBT : Tree
{
    public static float attackRange = 1f;
    public static float attackCooldown = 1f;
    public static string attackType = "spore";
    private EnemySpriteFlip Flip;

    protected override Node SetupTree()
    {
        Unit unit = GetComponent<Unit>();
        HealthComponent healthComponent = GetComponent<HealthComponent>(); 
        // I added this line bernas
        unit.target = GameObject.FindGameObjectWithTag("Player").transform;
        Flip = new EnemySpriteFlip(transform);

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInAttackRange(transform, attackRange, unit, attackType, healthComponent),
                //new TaskCheckCollision(transform),
                new TaskAttack(transform, unit, attackType, attackCooldown),
            }),
            new Sequence(new List<Node>
            {
                //new AimSpear(transform),
                new TaskGoToTarget(transform, unit, healthComponent),
                //new TaskSporeDefense(unit, healthComponent, isMoving),
            }),
        });
        root.SetData("target", GameObject.FindGameObjectWithTag("Player").transform);

        return root;
    }

    private void FixedUpdate()
    {
        Flip.Update();
    }
}

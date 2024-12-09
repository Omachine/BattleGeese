using System;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class AppleBT : Tree
{
    public float dashSpeed = 6f;
    public float attackRange = 5f;
    public float attackCooldown = 4f;
    private Animator _animator;
    Unit unit;

    private EnemySpriteFlip Flip;

    protected override Node SetupTree()
    {
        unit = GetComponent<Unit>();
        unit.target = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        EnemyHealthComponent healthComponent = GetComponent<EnemyHealthComponent>();
        Flip = new EnemySpriteFlip(transform);

        Node root = new Selector(new List<Node>
        {
            //new InterruptionNode(
            //    new CheckStunned(healthComponent),
            //    new Sequence(new List<Node>
            //    {
            //        new StopFollowingTargetTask(unit, _animator),
            //        new WaitTask(0.3f),
            //        new UnStun(healthComponent),
            //        new StartFollowingTargetTask(unit.target, unit, _animator),
            //    }),
                new Sequence(new List<Node>
                {
                    new WaitTask(attackCooldown),
                    new Sequence(new List<Node>{
                        new TaskCheckDistance(unit, attackRange, CheckType.inside),
                        new CheckTargetInSight(unit),
                        new TaskCheckDistance(unit, 1.5f, CheckType.outside),
                        new DashAttackTask(unit, 0.5f, 1f, dashSpeed, _animator),
                    }),
                })
          //  ),
        });
        
        return root;
    }

    private void FixedUpdate()
    {
        Flip.Update();
    }
}
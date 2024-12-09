using System;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class BrocolliBT : Tree
{
    private Transform _target;
    private Weapon _weapon;
    private Unit _unit;
    private Animator _animator;
    public static float attackCooldown = 1f;
    private EnemySpriteFlip Flip;

    protected override Node SetupTree()
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        _unit = GetComponent<Unit>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _unit.target = _target;
        _weapon = transform.Find("Weapon").GetComponent<Weapon>();
        _weapon.Equip();
        Flip = new EnemySpriteFlip(transform);
        EnemyHealthComponent healthComponent = GetComponent<EnemyHealthComponent>();

        Node root = new Selector(new List<Node>
        {
            new InterruptionNode(
                new CheckStunned(healthComponent),
                new Sequence(new List<Node>
                {
                    new StopFollowingTargetTask(_unit, _animator),
                    new WaitTask(0.3f),
                    new UnStun(healthComponent),
                }),
                
                new Sequence(new List<Node>
                {
                    new Sequence(new List<Node>
                    {                    
                        new WaitTask(attackCooldown),
                        new WeaponAttackTask(_unit, _weapon, _target),
                    }),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new TaskCheckDistance(_unit, 2f),
                            new StopFollowingTargetTask(_unit, _animator),
                        }),
                        new StartFollowingTargetTask(_target, _unit, _animator),
                    }),
                })
            ),
        });
        
        root.SetData("target", GameObject.FindGameObjectWithTag("Player").transform);

        return root;
    }

    private void FixedUpdate()
    {
        Flip.Update();
    }
}

using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class SushiBT : Tree
{
    private Transform _target;
    private Weapon _weapon;
    private Unit _unit;
    private Animator _animator;
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
        
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskCheckDistance(_unit, 0.8f),
                new StopFollowingTargetTask(_unit, _animator),
                new WeaponAttackTask(_unit, _weapon, _target),
                new WaitTask(1f),
            }),
            new StartFollowingTargetTask(_target, _unit, _animator),
        });

        return root;
    }

    private void FixedUpdate()
    {
        Flip.Update();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class OtoutoBt : Tree
{
    private Transform _target;
    private Weapon _weapon1;
    private Weapon _weapon2;
    private Unit _unit;
    private Animator _animator;
    public bool hasDashes = true;
    public bool isAgresive;
    private Transform _avoidant;
    [SerializeField] private LayerMask wallLayerMask; // Layer mask to detect walls

    protected override Node SetupTree()
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        _unit = GetComponent<Unit>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _avoidant = GameObject.FindGameObjectWithTag("Avoidant").transform;
        _unit.target = _target;
        _weapon1 = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        _weapon2 = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        _weapon1.Equip();
        _weapon2.Equip();

        EnemyHealthComponent healthComponent = GetComponent<EnemyHealthComponent>();

        DashAttackTask dashAttackTask = new DashAttackTask(_unit, 1f, 1f, 4, _animator);
        dashAttackTask.OnDashCompleted += () => hasDashes = false; // Subscribe to the event

        Node root = new Selector(new List<Node>
        {
            // Select between (sequence for CheckIsAgressive and Sequence Aggressive) and Sequence for Avoid
            // Dash only happens at start of aggressive or avoidant sequence
            // Condition for if hasDash is true, then dash then is false
            // If hasDash is false, then perform other actions

            new Sequence(new List<Node>()
            {
                new ConditionalNode(() => hasDashes),


                dashAttackTask, // Use the created DashAttackTask instance
            }),

            new Sequence(new List<Node>
            {
                // Check is aggressive
                new ConditionalNode(() => isAgresive),

                new Sequence(new List<Node>
                {
                    // Follow target
                    new StartFollowingTargetTask(_target, _unit, _animator),
                    
                    // Melee Attack
                    new WeaponAttackTask(_unit, _weapon1, _target),

                    new WaitTask(1f),
                }),
            }),

            // Avoid
            new Sequence(new List<Node>
            {
                new ConditionalNode(() => !isAgresive),

                new Sequence(new List<Node>
                {
                    new StartFollowingTargetTask(_avoidant, _unit, _animator),
                 

                    // Target in this case is the brother position for the TaskUpdateAvoidantPosition
                    new TaskUpdateAvoidantPosition(_avoidant, wallLayerMask, _unit.transform),
                    new WeaponAttackTask(_unit, _weapon2, _target),

                    new WaitTask(1f),
                }),
            }),
        });

        root.SetData("target", GameObject.FindGameObjectWithTag("Player").transform);

        return root;
    }
}








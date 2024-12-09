using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class CheckStunned : Node
{
    private EnemyHealthComponent _healthComponent;

    public CheckStunned(EnemyHealthComponent healthComponent)
    {
        _healthComponent = healthComponent;
    }

    public override NodeState Evaluate()
    {
        if (_healthComponent.IsStunned)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}

public class UnStun : Node
{
    private EnemyHealthComponent _healthComponent;

    public UnStun(EnemyHealthComponent healthComponent)
    {
        _healthComponent = healthComponent;
    }

    public override NodeState Evaluate()
    {
        _healthComponent.IsStunned = false;
        
        return NodeState.SUCCESS;
    }
}
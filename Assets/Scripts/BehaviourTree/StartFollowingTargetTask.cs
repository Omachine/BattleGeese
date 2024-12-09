using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class StartFollowingTargetTask : Node
{
    private Transform _target;
    private Unit _unit;
    private Animator _animator;

    public StartFollowingTargetTask(Transform target, Unit unit, Animator animator)
    {
        _target = target;
        _unit = unit;
        _animator = animator;
    }

    public override NodeState Evaluate()
    {
        _animator.SetBool("isWalking", true);
        _unit.target = _target;
        _unit.isStopped = false;
        
        state = NodeState.SUCCESS;
        return state;
    }
}
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class StopFollowingTargetTask : Node
{
    private Unit _unit;
    private Animator _animator;

    public StopFollowingTargetTask(Unit unit, Animator animator)
    {
        _unit = unit;
        _animator = animator;
    }

    public override NodeState Evaluate()
    {
        _animator.SetBool("isWalking", false);
        _unit.isStopped = true;
        _unit.rb.velocity = Vector3.zero;
        
        state = NodeState.SUCCESS;
        return state;
    }
}
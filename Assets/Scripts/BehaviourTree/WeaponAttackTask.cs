using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class WeaponAttackTask : Node
{
    private Transform _target;
    private Weapon _weapon;
    private Unit _unit;

    public WeaponAttackTask(Unit unit, Weapon weapon, Transform target)
    {
        _target = target;
        _weapon = weapon;
        _unit = unit;
    }

    public override NodeState Evaluate()
    {
        Vector3 direction = _target.transform.position - _unit.transform.position;

        _weapon.Direction = direction;
        _weapon.FlipWeaponSprite(direction.x < 0 ? Facing.left : Facing.right);
        _weapon.Enter();

        state = NodeState.SUCCESS;
        return state;
    }
}

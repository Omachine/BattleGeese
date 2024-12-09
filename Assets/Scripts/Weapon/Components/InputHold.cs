using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHold : WeaponComponent
{
    private Animator _animator;

    private bool _input;

    private void HandleInputChange(bool newInput)
    {
        _input = newInput;

        SetAnimatorParameter();
    }

    private void SetAnimatorParameter()
    {
        _animator.SetBool("hold", _input);
    }

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponentInChildren<Animator>();

        weapon.OnCurrentInputChange += HandleInputChange;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        weapon.OnCurrentInputChange -= HandleInputChange;
    }
}

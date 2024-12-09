using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum Facing
{
    right = 1,
    left = -1,
}

public class Weapon : MonoBehaviour
{
    public event Action<bool> OnCurrentInputChange;
    
    [HideInInspector] public WeaponGenerator _generator;

    private WeaponDataSO _data;

    public WeaponDataSO Data
    {
        get => _data;
        set
        {
            _data = value;
        }
    }

    public void GenerateWeapon() => _generator.GenerateWeapon(_data);

    public float AnimationSpeedMultiplier = 1.0f;

    public bool CurrentInput
    {
        get => _currentInput;
        set
        {
            if (_currentInput != value)
            {
                _currentInput = value;
                OnCurrentInputChange?.Invoke(_currentInput);
            }
        }
    }

    // public Player Character { get; private set; }

    public event Action OnEnter;
    public event Action OnExit;

    private Vector3 _direction;
    
    public Facing Facing { get; private set; } = Facing.right;

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = new Vector3(value.x, 0f, value.z);
    }

    public Vector3 TargetPosition => transform.position + Direction;

    private Animator _animator;
    public GameObject BaseGameObject { get; private set; }

    public AnimationEventHandler EventHandler { get; private set; }
    public HealthComponent User { get; set; }

    private bool _currentInput;

    public void Equip()
    {
        if (_data != null) _animator.SetBool("equiped", true);
    }

    public void Unequip()
    {
        if (_data != null) _animator.SetBool("equiped", false);
    }

    public void Enter()
    {
        _animator.SetBool("active", true);
        
        _animator.speed = AnimationSpeedMultiplier;

        OnEnter?.Invoke();
    }

    public void SetData(WeaponDataSO data) => Data = data;

    public void Exit()
    {
        _animator.SetBool("active", false);

        OnExit?.Invoke();
    }

    private void Awake()
    {
        _generator = GetComponent<WeaponGenerator>();
        BaseGameObject = transform.Find("Base").gameObject;
        _animator = BaseGameObject.GetComponent<Animator>();
        User = transform.parent.GetComponent<HealthComponent>();

        EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
    }

    private void OnEnable() => EventHandler.OnFinish += Exit;
    private void OnDisable() => EventHandler.OnFinish -= Exit;

    public void FlipWeaponSprite(Facing facing)
    {
        Facing = facing;
        transform.localScale = new Vector3((int)Facing, 1, 1);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, TargetPosition);
    }
}

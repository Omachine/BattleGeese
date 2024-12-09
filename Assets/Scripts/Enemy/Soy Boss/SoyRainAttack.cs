using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class SoyRainAttack : Node
{
    private Transform _transform;
    private float _timer;
    private float _timer2;
    private float _shootDelay;
    private float _speed;
    private float _damage;
    private float _duration;
    private SoySpawner _soySpawner;

    public SoyRainAttack(float shootDelay, float shootSpeed, float damage, float duration)
    {
        _shootDelay = shootDelay;
        _speed = shootSpeed;
        _damage = damage;
        _duration = duration;
        _soySpawner = GameObject.Find("SoySpawner").GetComponent<SoySpawner>();
    }
    
    public override NodeState Evaluate()
    {
        _timer += Time.deltaTime;
        _timer2 += Time.deltaTime;

        if (_timer2 > _duration)
        {
            _timer = 0f;
            _timer2 = 0f;
            return NodeState.SUCCESS;
        }
        
        if (_timer > _shootDelay)
        {
            _soySpawner.Spawn(_speed, _damage);
            _timer -= _shootDelay;
        }

        state = NodeState.RUNNING;
        return state;
    }
}

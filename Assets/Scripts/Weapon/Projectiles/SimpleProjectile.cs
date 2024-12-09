using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : BasePorjectile
{
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    
    private Vector3 _velocity;
    private Transform _spriteTransform;

    public override void Spawn(Vector3 position, Vector3 target, float speed, float damage, LayerMask layerMask, HealthComponent sender)
    {
        base.Spawn(position, target, speed, damage, layerMask, sender);
        
        _rigidbody = GetComponent<Rigidbody>();
        
        Vector3 distance = target - position;
        
        _velocity = distance.normalized * speed;

        _rigidbody.velocity = _velocity;
        
        _spriteTransform = transform.Find("Sprite");

        _spriteTransform.transform.right = _velocity;
    }
}

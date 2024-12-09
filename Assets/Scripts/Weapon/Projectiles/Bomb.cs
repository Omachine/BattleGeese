using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : BasePorjectile
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject _explosionGameObject;
    
    private bool hasExploded;
    
    private float _duration;
    
    // x(t) = x + vxt
    // y(t) = y + vyt - ayt²
    private Vector3 _velocity;

    private float vy0;
    private Rigidbody _rigidbody;

    public override void Spawn(Vector3 position, Vector3 target, float speed, float damage, LayerMask layerMask, HealthComponent sender)
    {
        base.Spawn(position, target, speed, damage, layerMask, sender);
        
        Vector3 distance = target - position;
        
        _velocity = distance.normalized * speed;
        
        // tempo = distancia / velocidade
        _duration = distance.magnitude / speed;
        
        // vy0
        _velocity.y = -Physics.gravity.y / 2 * _duration;
        
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.velocity = _velocity;
    }

    private void Update()
    {
        if (transform.position.y < 0f) Explode();
    }

    private void Explode()
    {
        if (hasExploded) return;
        
        hasExploded = true;
        
        if (_explosionGameObject)
            Instantiate(_explosionGameObject, transform.position, transform.rotation);
        
        DamageUtil.DamageInRadius(transform.position, radius, damage, layerMask);
        Destroy(gameObject);
    }

    protected override void Hit(Collider other) {}
}
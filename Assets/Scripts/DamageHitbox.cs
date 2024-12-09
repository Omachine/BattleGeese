using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private float _damageCooldown = 0.8f;
    private float _timer;
    public DamageType Da = DamageType.None;
    public LayerMask layerMask;


    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _damageCooldown) return;

        _timer -= _damageCooldown;

        DamageUtil.DamageInRadius(transform.position, _damageRadius, _damage, layerMask, Da);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Damage(other);
    // }
    //
    // private void Damage(Collider other)
    // {
    //     if (other.TryGetComponent(out IDamageable player))
    //     {
    //         player.Damage(_damage, other.transform.position - transform.position);
    //     }
    // }
}

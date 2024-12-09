using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public void Spawn(Vector3 position, Vector3 target, float speed, float damage, LayerMask layerMask, HealthComponent sender = null);
}

public abstract class BasePorjectile : MonoBehaviour, IProjectile
{
    private const float lifeTime = 3f;

    protected Vector3 origin;
    protected float damage;
    protected Vector3 target;
    protected float speed;
    protected LayerMask layerMask;
    protected HealthComponent _sender;

    public virtual void Spawn(
        Vector3 position,
        Vector3 target,
        float speed,
        float damage,
        LayerMask layerMask,
        HealthComponent sender)
    {
        _sender = sender;
        
        transform.position = position;
        origin = position;
        this.damage = damage;
        this.target = target;
        this.speed = speed;
        this.layerMask = layerMask;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.excludeLayers = ~layerMask;
        
        Destroy(gameObject, lifeTime);
    }

    protected virtual void Hit(Collider other)
    {
        // Maybe add a particle effect later
        
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(damage, (target - origin).normalized, DamageType.None, _sender);
        }
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other) => Hit(other);
}

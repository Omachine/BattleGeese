
using UnityEngine;

public class Damage : WeaponComponent<DamageData, AttackDamage>
{
    private ActionHitbox _hitbox;

    private void HandleDetectCollider(Collider[] colliders)
    {
        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(
                    currentAttackData.Amount,
                    new Vector3((int)weapon.Facing, 0, 0),
                    currentAttackData.Type,
                    weapon.User
                    );
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _hitbox = GetComponent<ActionHitbox>();
    }

    protected override void Start()
    {
        base.Start();

        _hitbox.OnDetectedCollider += HandleDetectCollider;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _hitbox.OnDetectedCollider -= HandleDetectCollider;
    }
}
using UnityEngine;

public enum DamageType
{
    None,
    Stun,
    burn,
    freezing,
}

public interface IDamageable
{
    void Damage(float amount, Vector3 direction, DamageType type = DamageType.None, HealthComponent source = null, float duration = 0f);
}

public static class DamageUtil
{
    // 20 is 20...
    private static Collider[] _colliders = new Collider[20];

    public static void DamageInRadius(Vector3 position,
        float radius,
        float damage,
        LayerMask layerMask,
        DamageType damageType = DamageType.None
        )
    {
        var results = Physics.OverlapSphereNonAlloc(position, radius, _colliders, layerMask);

        if (results == 0) return;

        for (int i = 0; i < results; i++)
        {
            if (_colliders[i].TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage, _colliders[i].transform.position - position, damageType);
            }
        }
    }
}

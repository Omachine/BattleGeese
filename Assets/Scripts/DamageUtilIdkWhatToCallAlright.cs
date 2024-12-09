using UnityEngine;

namespace DamageUtilIdkWhatToCallAlright
{
    public class DamageStuffIGuess
    {
        public void DamageInradius(Vector3 position, ref Collider[] colliders, float radius, float damage, LayerMask layerMask)
        {
            Physics.OverlapSphereNonAlloc(position, radius, colliders, layerMask);

            if (colliders.Length == 0) return;
        
            foreach (Collider player in colliders)
            {
                if (player.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(damage, (player.transform.position - position).normalized, DamageType.None);
                }
            }
        }
    }
}
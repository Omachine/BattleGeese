using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Prop : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _damageParticles;

    public void Damage(float amount, Vector3 direction, DamageType type, HealthComponent source, float duration)
    {
        _damageParticles.Play();
        // Play particle effect
        //Destroy(gameObject);
    }
}

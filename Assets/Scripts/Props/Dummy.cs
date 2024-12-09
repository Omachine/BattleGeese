using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Dummy : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _damageParticles;
    private Animator _animator;

    public void Damage(float amount, Vector3 direction, DamageType type, HealthComponent source, float duration)
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        _animator.SetTrigger("damage");
        _damageParticles.Play();
        
    }
}

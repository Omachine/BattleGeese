using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class ExplosionAOE : BasePorjectile
{
    public float Radius;
    public GameObject ExplosionVFX;
    VisualEffect explosionVFXInstance;
    
    public float Delay;
    
    public override void Spawn(Vector3 position, Vector3 target, float speed, float damage, LayerMask layerMask, HealthComponent sender)
    {
        base.Spawn(position, target, speed, damage, layerMask, sender);

        transform.position = target;

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        explosionVFXInstance = Instantiate(ExplosionVFX, transform.position, Quaternion.identity).GetComponent<VisualEffect>();
        // explosionVFXInstance.Play();
        yield return new WaitForSeconds(Delay);
        DamageUtil.DamageInRadius(transform.position, Radius, damage, layerMask, DamageType.Stun);
    }
}

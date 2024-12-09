using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseBall : BasePorjectile
{
    private const float maxRecoilAngle = 10f;
    
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    
    private Vector3 _velocity;

    public override void Spawn(Vector3 position, Vector3 target, float speed, float damage, LayerMask layerMask, HealthComponent sender)
    {
        base.Spawn(position, target, speed, damage, layerMask, sender);
        
        _rigidbody = GetComponent<Rigidbody>();
        
        Vector3 distance = target - position;
        
        _velocity = distance.normalized * speed;

        // Rotaciona o vetor
        Vector3 rotatedVector =Quaternion.AngleAxis(
            Random.Range(-maxRecoilAngle, maxRecoilAngle),
            Vector3.up) * _velocity;
        
        _rigidbody.velocity = rotatedVector;
    }
}

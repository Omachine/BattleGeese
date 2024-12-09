using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleProjectile : MonoBehaviour, IProjectile
{
    public GameObject projectilePrefab;
    private const float spread = 10f;
    
    public void Spawn(Vector3 position, Vector3 target, float speed, float damage, LayerMask layerMask, HealthComponent sender)
    {
        GameObject left = Instantiate(projectilePrefab, position, Quaternion.identity);
        GameObject middle = Instantiate(projectilePrefab, position, Quaternion.identity);
        GameObject right = Instantiate(projectilePrefab, position, Quaternion.identity);

        // Rotaciona o vetor
        Vector3 leftTarget = (Quaternion.AngleAxis(-spread, Vector3.up) * (target - position)) + position;
        Vector3 rightTarget = (Quaternion.AngleAxis(spread, Vector3.up) * (target - position)) + position;
        
        left.GetComponent<IProjectile>().Spawn(
            position,
            leftTarget,
            speed,
            damage,
            layerMask,
            sender
        );
        middle.GetComponent<IProjectile>().Spawn(
            position,
            target,
            speed,
            damage,
            layerMask,
            sender
        );
        right.GetComponent<IProjectile>().Spawn(
            position,
            rightTarget,
            speed,
            damage,
            layerMask,
            sender
        );
    }
}

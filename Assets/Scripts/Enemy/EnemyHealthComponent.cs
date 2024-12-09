using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyHealthComponent : HealthComponent
{
    // This action is to inform the room to check if it was cleared by the player
    public static event Action OnEnemyDeath;

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnEnemyDeath?.Invoke();
    }
}

using System;
using UnityEngine;

[Serializable]
public class AttackDamage : AttackData
{
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public DamageType Type { get; private set; } = DamageType.None;
}

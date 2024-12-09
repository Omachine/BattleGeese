using System;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class AttackEffect : AttackData
{
    [field: SerializeField] public GameObject EffectObject;
    [field: SerializeField] public bool shouldNotRotate;
}

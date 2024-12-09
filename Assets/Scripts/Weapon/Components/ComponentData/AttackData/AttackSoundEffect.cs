using System;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class AttackSoundEffect : AttackData
{
    [field: SerializeField] public float HitSoundClipVolume;
    [field: SerializeField] public AudioClip HitSoundClip;
}

using UnityEngine;
using UnityEngine.VFX;

public class SoundEffectComponent : WeaponComponent<SoundEffectData, AttackSoundEffect>
{
    private void PlaySoundEffect()
    {
        SoundManager.instance.PlayClip(currentAttackData.HitSoundClip, transform, currentAttackData.HitSoundClipVolume);
    }

    protected override void Start()
    {
        base.Start();

        EventHandler.OnHitSound += PlaySoundEffect;
    }

    protected override void OnDestroy()
    {
        EventHandler.OnHitSound -= PlaySoundEffect;
        
        base.OnDestroy();
    }
}

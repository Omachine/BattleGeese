using System;
using UnityEngine;
using UnityEngine.VFX;

public class Effect : WeaponComponent<EffectData, AttackEffect>
{
    VisualEffect VisualEffect;
    GameObject visualEffectObject;

    private void PlayVisualEffect() => VisualEffect.Play();

    private void StopAnimation() => VisualEffect.Stop();

    protected override void Start()
    {
        base.Start();

        visualEffectObject = Instantiate(data.AttackData.EffectObject, transform);

        VisualEffect = visualEffectObject.GetComponent<VisualEffect>();

        EventHandler.OnAttack += PlayVisualEffect;
        EventHandler.OnFinish += StopAnimation;
    }

    protected override void OnDestroy()
    {
        EventHandler.OnAttack -= PlayVisualEffect;
        EventHandler.OnFinish -= StopAnimation;
        if(VisualEffect!= null) Destroy(VisualEffect.gameObject);
        
        base.OnDestroy();
    }

    private void Update()
    {
        if (data.AttackData.shouldNotRotate) visualEffectObject.transform.localScale = new Vector3((int)weapon.Facing, 1, 1);
    }
}

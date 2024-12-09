using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnStep;
    public event Action OnHitSound;
    public event Action OnAttack;
    public event Action<AttackPhases> OnEnterAttackPhase;

    private void AnimationFinishTrigger() => OnFinish?.Invoke();
    private void StepTrigger() => OnStep?.Invoke();
    private void AttackTrigger() => OnAttack?.Invoke();
    private void HitSoundTrigger() => OnHitSound?.Invoke();
    private void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
}

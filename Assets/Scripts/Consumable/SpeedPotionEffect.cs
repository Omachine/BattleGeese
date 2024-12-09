using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedPotionEffect", menuName = "Data/Consumable Effects/Speed Potion Effect", order = 1)]
public class SpeedPotionEffect : ConsumableEffect
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float duration = 5f;

    public override void ApplyEffect(Player player)
    {
        player.StartCoroutine(ApplySpeedBoost(player));
    }

    private IEnumerator ApplySpeedBoost(Player player)
    {
        player.StateMachine.ReusableData.BonusSpeedMultiplier *= speedMultiplier;
        Debug.Log($"Speed potion used. Player speed increased by {speedMultiplier}x for {duration} seconds.");
        yield return new WaitForSeconds(duration);
        player.StateMachine.ReusableData.BonusSpeedMultiplier /= speedMultiplier;
        Debug.Log("Speed boost ended.");
    }
}

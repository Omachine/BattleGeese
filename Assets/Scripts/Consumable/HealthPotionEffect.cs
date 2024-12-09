using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotionEffect", menuName = "Data/Consumable Effects/Health Potion Effect", order = 0)]
public class HealthPotionEffect : ConsumableEffect
{
    [SerializeField] private float healAmount = 20f;

    public override void ApplyEffect(Player player)
    {
        player.GetComponent<HealthComponent>().Health += healAmount;
        Debug.Log($"Health potion used. Player healed by {healAmount}.");
    }
}

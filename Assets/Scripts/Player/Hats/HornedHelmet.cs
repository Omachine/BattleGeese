using UnityEngine;

public class HornedHelmet : HatBehaviour
{
    private float AttackSpeedMultiplier = 0.35f;
    
    protected override void Equip()
    {
        player.AttackSpeed += AttackSpeedMultiplier;
    }

    protected override void Unequip()
    {
        player.AttackSpeed -= AttackSpeedMultiplier;
    }
}
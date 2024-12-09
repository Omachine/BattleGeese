using UnityEngine;

public class HelmOfHonor : HatBehaviour
{
    private int _extraHealth;
    
    protected override void Equip()
    {
        player.GetComponent<HealthComponent>().MaxHealth += _extraHealth;
    }

    protected override void Unequip()
    {
        player.GetComponent<HealthComponent>().MaxHealth -= _extraHealth;
    }
}
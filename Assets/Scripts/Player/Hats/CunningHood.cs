﻿using UnityEngine;

public class CunningHood : HatBehaviour
{
    private float lifeStealPercentage = 0.5f;
    
    protected override void Equip()
    {
        player.GetComponent<HealthComponent>().LifeSteal += lifeStealPercentage;
    }

    protected override void Unequip()
    {
        player.GetComponent<HealthComponent>().LifeSteal -= lifeStealPercentage;
    }
}
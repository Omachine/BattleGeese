using UnityEngine;

public class SpeedsterCap : HatBehaviour
{
    private float speedMultiplier = 0.5f;
    
    protected override void Equip()
    {
        player.StateMachine.ReusableData.BonusSpeedMultiplier += speedMultiplier;
    }

    protected override void Unequip()
    {
        player.StateMachine.ReusableData.BonusSpeedMultiplier -= speedMultiplier;
    }
}
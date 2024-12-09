using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerMovementState
{
    private float _timeElapsed;
    
    Material defaultMaterial;
    Material stunnedMaterial;

    public PlayerStunnedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        defaultMaterial = player.SpriteRenderer.material;
    }

    public override void Enter()
    {
        reusableData.SpeedMultiplier = 0f;
        
        _timeElapsed = 0f;
        
        player.SpriteRenderer.material = player.Data.FlashMaterial;

        // TODO Switch player sprite to stunned
    }

    public override void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > player.Data.StunDuration)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
    }

    public override void Exit()
    {
        player.SpriteRenderer.material = defaultMaterial;
    }
}

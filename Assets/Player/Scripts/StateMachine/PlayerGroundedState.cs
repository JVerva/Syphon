using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine context, PlayerStateFactory factory, bool isRootState) : base(context, factory, isRootState) { }

    protected override void CheckSwitchState()
    {
        if(!_context.IsGrounded){
            SwitchState(_factory.getAerialState());
        }
    }

    protected override void EnterState()
    {
        
    }

    protected override void ExitState()
    {
        
    }

    protected override void InitializeSubState()
    {
        SetSubState(_factory.getIdleState());
    }

    protected override void UpdateState()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAerialState : PlayerState
{
    public PlayerAerialState(PlayerStateMachine context, PlayerStateFactory holder, bool isRootState) : base(context, holder, isRootState) { }

    public PlayerAerialState(PlayerStateMachine context, PlayerStateFactory holder, bool isRootState, Vector3 initialVelocity) : base(context, holder, isRootState) { }

    protected override void CheckSwitchState()
    {
        if(_context.IsGrounded){
            SwitchState(_factory.getGroundedState());
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

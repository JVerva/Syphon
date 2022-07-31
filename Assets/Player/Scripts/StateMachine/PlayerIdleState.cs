using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine context, PlayerStateFactory factory, bool isRootState) : base(context, factory, isRootState) { }
    protected override void CheckSwitchState()
    {
        if(_context.IsMovePressed){
            SwitchState(_factory.getWalkState());
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
        
    }

    protected override void UpdateState()
    {
        
    }
}

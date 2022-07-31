using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerStateMachine context, PlayerStateFactory factory, bool isRootState) : base(context, factory, isRootState) { }
    protected override void CheckSwitchState()
    {
        if(!_context.IsMovePressed){
            SwitchState(_factory.getIdleState());
        }
        if(_context.IsRunPressed){
            SwitchState(_factory.getRunState());
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

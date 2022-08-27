using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory holder, bool isRootState) : base(context, holder, isRootState) { }

    protected override void CheckSwitchState()
    {
        if(!_context.IsMovePressed){
            SwitchState(_factory.getIdleState());
        }
        if(!_context.IsRunPressed){
            SwitchState(_factory.getWalkState());
        }
    }

    protected override void EnterState()
    {
        _context.TargetMoveSpeed = PlayerStats.runSpeed;
    }

    protected override void ExitState()
    {
        _context.TargetMoveSpeed = 0f;
    }

    protected override void InitializeSubState()
    {

    }

    protected override void UpdateState()
    {

    }

}

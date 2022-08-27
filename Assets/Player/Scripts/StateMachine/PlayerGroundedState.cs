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
        _context.PushDown = Vector3.zero;
    }

    protected override void InitializeSubState()
    {
        SetSubState(_factory.getIdleState());
    }

    protected override void UpdateState()
    {
        GetGroundNormal();
        GetGroundAngle();
        //sets push down vector so that the is grounded remains true
        _context.PushDown = _context.GroundNormal * -0.1f;
    }

    //gets the ground normal vector
    void GetGroundNormal()
    {
        if ((_context.CharacterController.collisionFlags & CollisionFlags.Below) != 0) {
            _context.GroundNormal = _context.Hit.normal;
        }
    }

    //get the ground angle
    void GetGroundAngle()
    {
        _context.GroundAngle = Vector3.Angle(Vector3.up, _context.GroundNormal);
    }
}

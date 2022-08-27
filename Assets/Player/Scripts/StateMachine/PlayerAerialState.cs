using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAerialState : PlayerState
{
    public PlayerAerialState(PlayerStateMachine context, PlayerStateFactory holder, bool isRootState) : base(context, holder, isRootState) { }

    public PlayerAerialState(PlayerStateMachine context, PlayerStateFactory holder, bool isRootState, float initialVerticalSpeed) : base(context, holder, isRootState) {
        _initialVerticalSpeed = initialVerticalSpeed;
    }

    private float _initialVerticalSpeed;
    private float _verticalSpeed;
    private Vector2 _horizontalVelocity;
    protected override void CheckSwitchState()
    {
        if(_context.IsGrounded){
            SwitchState(_factory.getGroundedState());
        }
    }

    protected override void EnterState()
    {
        _context.Velocity = new Vector3(_context.Velocity.x, _initialVerticalSpeed, _context.Velocity.z);
    }

    protected override void ExitState()
    {
        _verticalSpeed = 0;
    }

    protected override void InitializeSubState()
    {
        SetSubState(_factory.getIdleState());
    }

    protected override void UpdateState()
    {
        _verticalSpeed -= _context.g*Time.deltaTime;
        _verticalSpeed = Mathf.Clamp(_verticalSpeed, -67,67);
        _horizontalVelocity = new Vector2(_context.Velocity.x, _context.Velocity.z);
        _horizontalVelocity = Vector2.Lerp(_horizontalVelocity, Vector2.zero, _context.AirResistance/Vector2.Distance(_horizontalVelocity,Vector2.zero) * Time.deltaTime);
        _context.Velocity = new Vector3(_horizontalVelocity.x , _verticalSpeed, _horizontalVelocity.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerStateMachine context, PlayerStateFactory factory, bool isRootState) : base(context, factory, isRootState) { }
    protected override void CheckSwitchState()
    {
        if(_context.Velocity.magnitude==0){
            SwitchState(_factory.getIdleState());
        }
        if(_context.IsRunPressed){
            SetSubState(_factory.getRunState());
        }
    }

    protected override void EnterState()
    {
        _context.TargetMoveSpeed = PlayerStats.walkSpeed;
        Debug.Log(_context.TargetMoveSpeed);
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
        float planeY = (-_context.GroundNormal.x * _context.MoveDirection.x - _context.GroundNormal.z * _context.MoveDirection.z) / _context.GroundNormal.y;
        if (planeY < 0)
        {
            _context.MoveDirection = new Vector3(_context.MoveDirection.x, planeY, _context.MoveDirection.z).normalized;
            _context.TargetVelocity = _context.MoveDirection * _context.TargetMoveSpeed;
        }
        else
        {
            _context.TargetVelocity = _context.MoveDirection * _context.TargetMoveSpeed * Mathf.Pow(Mathf.Cos(_context.GroundAngle / PlayerStats.maxWalkAngle), (2 * Vector3.Dot(_context.MoveDirection, new Vector3(-_context.GroundNormal.x, 0, -_context.GroundNormal.z).normalized)));
        }
        if (_context.TargetVelocity.magnitude > _context.Velocity.magnitude)
            _context.Accelaration = PlayerStats.accelaration;
        else
            _context.Accelaration = PlayerStats.stopAccelaration;
        _context.Velocity = Vector3.Lerp(_context.Velocity, _context.TargetVelocity, _context.Accelaration / (_context.Velocity - _context.TargetVelocity).magnitude * Time.deltaTime);
        Debug.Log(_context.TargetVelocity);
        Debug.DrawLine(_context.transform.position + Vector3.up, _context.transform.position + _context.TargetVelocity * 2 + Vector3.up, Color.green, 0);
    }

}

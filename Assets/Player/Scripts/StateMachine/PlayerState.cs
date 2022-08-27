using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine _context;
    protected PlayerState _currentSubState;
    protected PlayerState _currentSuperState;
    protected PlayerStateFactory _factory;
    protected bool _isRootState;

    public PlayerState(PlayerStateMachine context, PlayerStateFactory factory, bool isRootState)
    {
        _context = context;
        _factory = factory;
        _isRootState = isRootState;
        InitializeSubState();
    }

    //run when entering the state
    protected abstract void EnterState();

    //run every fixed frame when player is in state
    protected abstract void UpdateState();

    //run when exiting the state
    protected abstract void ExitState();

    //check conditions to switch to a different state
    protected abstract void CheckSwitchState();

    //sets the default substate
    protected abstract void InitializeSubState();

    //update this and all substate's states
    public void UpdateStates()
    {
        CheckSwitchState();
        UpdateState();
        if(_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    //set the current substate
    protected void SetSubState(PlayerState subState)
    {
        _currentSubState = subState;
        _currentSubState.SetSuperState(this);
    }

    //set the current super state
    protected void SetSuperState(PlayerState superState)
    {
        _currentSuperState = superState;
    }

    //switch state in this point of the hierachy
    protected void SwitchState(PlayerState state)
    {
        ExitState();

        state.EnterState();

        if (_isRootState)
            _context.CurrentState = state;
        else if (_currentSuperState != null)
            _currentSuperState.SetSubState(state);
    }
}

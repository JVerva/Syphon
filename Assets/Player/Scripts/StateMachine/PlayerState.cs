using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    private PlayerStateMachine _context;
    private PlayerState _currentSubState;
    private PlayerState _currentSuperState;
    private StateHolder _holder;
    private bool _isRootState;

    public PlayerState(PlayerStateMachine context, StateHolder holder, bool isRootState)
    {
        _context = context;
        _holder = holder;
        _isRootState = isRootState;
        InitializeSubState();
    }

    protected abstract void EnterState();
    protected abstract void UpdateState();
    protected abstract void ExitState();
    protected abstract void CheckSwitchState();
    protected abstract void InitializeSubState();
    public void UpdateStates()
    {
        CheckSwitchState();
        UpdateState();
        if(_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }
    private void SetSubState(PlayerState subState)
    {
        _currentSubState = subState;

        _currentSubState.SetSuperState(this);
    }
    private void SetSuperState(PlayerState superState)
    {
        _currentSuperState = superState;
    }
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

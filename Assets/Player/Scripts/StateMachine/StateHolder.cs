using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHolder
{
    private PlayerStateMachine _context;
    private PlayerState _groudedState = null;
    private PlayerState _aerialState = null;
    private PlayerState _walkingState = null;
    private PlayerState _runningState = null;
    private PlayerState _idleState = null;

    public StateHolder(PlayerStateMachine context)
    {
        _context = context;
    }

    private PlayerState getGroundedState()
    {
        return new PlayerGroundedState(_context, this, true);
    }
    
    private PlayerState getAerialState()
    {
        return new PlayerAerialState(_context, this, true);
    }
    
    private PlayerState getWalkState()
    {
        return new PlayerWalkState(_context, this, false);
    }
    
    private PlayerState getRunState()
    {
        return new PlayerRunState(_context, this, false);
    }
    
    private PlayerState getIdleState()
    {
        return new PlayerIdleState(_context, this, false);
    }

}

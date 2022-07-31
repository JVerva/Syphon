using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine context)
    {
        _context = context;
    }

    public PlayerState getGroundedState()
    {
        return new PlayerGroundedState(_context, this, true);
    }
    
    public PlayerState getAerialState()
    {
        return new PlayerAerialState(_context, this, true);
    }
    
    public PlayerState getWalkState()
    {
        return new PlayerWalkState(_context, this, false);
    }
    
    public PlayerState getRunState()
    {
        return new PlayerRunState(_context, this, false);
    }
    
    public PlayerState getIdleState()
    {
        return new PlayerIdleState(_context, this, false);
    }

}

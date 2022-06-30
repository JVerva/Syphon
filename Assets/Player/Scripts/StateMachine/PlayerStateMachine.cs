using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState _currentState;

    public PlayerState CurrentState { get { return _currentState; } set { _currentState = value; } }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentState.UpdateStates();
    }

}

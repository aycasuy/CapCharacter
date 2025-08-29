using System;
using UnityEngine;

public class StateControl : MonoBehaviour
{
    private PlayerState _currentState = PlayerState.Idle;

    private void Start() {
        SetState(PlayerState.Idle);
    
    }
        

    public void SetState(PlayerState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        
    }

    public PlayerState GetCurrentState()
    {
        return _currentState;
    }

   
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    // Current State
    private State currentState;
    [HideInInspector]
    public string currentStateName { get; private set; }

    // Update, LateUpdate, FixedUpdate
    public void Update()
    {
        currentState?.Update();
    }
    public void LateUpdate()
    {
        currentState?.LateUpdate();
    }
    public void FixedUpdateUpdate()
    {
        currentState?.FixedUpdate();
    }
    // ChangeState
    public void ChangeState(State newState)
    {
        currentState?.Exit(); // o ? aqui substitui o if (currentState != null) {.........}
        currentState = newState;
        currentStateName = newState?.name;
        newState?.Enter();
    } 
}
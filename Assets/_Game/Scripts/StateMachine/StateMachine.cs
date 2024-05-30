using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StateMachine<T> where T : class
{
    [SerializeField] private T t;
    private IState<T> previousState;
    private IState<T> currentState;

    public StateMachine(T t)
    {
        this.t = t;
        currentState = null;
    }
    public void ChangeState(IState<T> newState)
    {
        if (currentState != null)
        {
            previousState = currentState;
            currentState.ExitState(t);
        }
        currentState = newState;
        currentState.EnterState(t);
    }
    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    } 
    public void ExecuteState()
    {
        if (currentState != null)
        {
            currentState.Execute(t);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    public void EnterState(T t);
    public void Execute(T t);
    public void ExitState(T t);
}
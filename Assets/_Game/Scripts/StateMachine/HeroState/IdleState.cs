using ChangeAnim;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class IdleState : IState<Hero>
{
    public void EnterState(Hero t)
    {
        t.ChangeAnim(Constants.ANIM_IDLE);
    }

    public void Execute(Hero t)
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {
            return;
        }
        t.UpdateTarget();
    }

    public void ExitState(Hero t)
    {
        
    }
}

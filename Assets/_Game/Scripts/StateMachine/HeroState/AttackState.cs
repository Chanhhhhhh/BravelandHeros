using ChangeAnim;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Hero>
{
    public void EnterState(Hero t)
    {
        t.ChangeAnim(Constants.ANIM_ATTACK);
    }

    public void Execute(Hero t)
    {
        
        t.UpdateTarget();
        t.LookTarget();
        t.CountDownAttack();
    }
    
    public void ExitState(Hero t)
    {
    }

}

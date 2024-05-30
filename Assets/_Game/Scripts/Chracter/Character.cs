using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : GameUnit
{
    [SerializeField] protected Animator Anim;
    protected string CurrentAnim;
    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {

    }
    public void ChangeAnim(string animName)
    {
        if (this.CurrentAnim != animName)
        {
            Anim.ResetTrigger(this.CurrentAnim);
            this.CurrentAnim = animName;
            Anim.SetTrigger(this.CurrentAnim);
        }
    }
}

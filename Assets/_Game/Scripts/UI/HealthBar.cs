using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : GameUnit
{
    [SerializeField] private Slider HpBar;
    private IHealth owner;

    public override void OnInit()
    {
        TF.SetParent(UIManager.Instance.GetUI<GamePlay>().transform);
    }

    public void SetUp( IHealth owner)
    {
        this.OnInit();
        this.owner = owner;
        HpBar.maxValue = owner.MaxHP;
        HpBar.value = owner.MaxHP;

    }

    private void Update()
    {
        if(owner != null)
        {
            TF.position = Cache.MainCamera.WorldToScreenPoint(owner.TF.position + new Vector3(0, 1.5f, 0));

        }
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void SetHealth(int Health)
    {
        HpBar.value = Health;
    }
}

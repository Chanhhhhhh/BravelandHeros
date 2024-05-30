using ChangeAnim;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character , IHealth
{
    private Transform target;
    [SerializeField] private float speed;
    private int waypointIndex;
    private HealthBar healthBar;

    [field: SerializeField] public int MaxHP { get; set; }
    public int hp;
    public bool isDie;
    bool IHealth.IsDie => isDie;

    public override void OnInit()
    {
        base.OnInit();       
        healthBar = SimplePool.Spawn<HealthBar>(PoolType.HealthBar);
        healthBar.SetUp(this);
        this.hp = MaxHP;
        ChangeAnim(Constants.ANIM_RUN);
        waypointIndex = 0;
        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {
            if (!this.CurrentAnim.Equals(Constants.ANIM_IDLE))
            {
                this.ChangeAnim(Constants.ANIM_IDLE);
            }
            return;
        }
        if (Vector3.Distance(TF.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        Vector3 dir = target.position - TF.position;
        TF.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        Quaternion newrotate = Quaternion.LookRotation(dir);
        TF.rotation = Quaternion.Slerp(TF.rotation, newrotate, Time.deltaTime * 20f);
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            LevelManager.Instance.ReduceHealth();
            OnDespawn();
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void TakeDamage(int damage)
    {
        if(hp <= damage)
        {
            hp  = 0;
            healthBar.SetHealth(hp);
            OnDespawn();
        }
        else
        {
            hp -= damage;
            healthBar.SetHealth(hp);
        }

    }

    public override void OnDespawn()
    {

        base.OnDespawn();     
        this.CurrentAnim = null;
        Spawner.Instance.DeleteEnemy(this);
        if (Spawner.Instance.IsWin())
        {
            LevelManager.Instance.Win();
        }

        SimplePool.Despawn(this.healthBar);
        SimplePool.Despawn(this);
        LevelManager.Instance.ReceiveCoin(50);
    }



}

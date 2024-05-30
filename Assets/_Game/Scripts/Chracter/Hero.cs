using ChangeAnim;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    protected IState<Hero> attackState = new AttackState();
    protected IState<Hero> idleState = new IdleState();
    private Enemy target;
    public float range = 15f;

    //public float turnSpeed = 10f;

    public PoolType TypeBullet;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [SerializeField]
    StateMachine<Hero> stateMachine;
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(idleState);
    }
     public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.TAG_ENEMY);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(TF.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.GetComponent<Enemy>();
            ChangeState(attackState);
        }
        else
        {
            target = null;
            ChangeState(idleState);
        }

    }

    public void ChangeState(IState<Hero> state)
    {
        stateMachine.ChangeState(state);
    }
    // Update is called once per frame
    void Update()
    {

        stateMachine.ExecuteState();
        if (!GameManager.IsState(GameState.GamePlay))
        {
            ChangeState(idleState);
        }   
    }


    public void CountDownAttack()
    { 
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }
    public void LookTarget()
    {
        if(target == null)
        {
            return;
        }
        Vector3 targetAngle = target.TF.position - this.TF.position;
        float targetAngleY = Mathf.Atan2(targetAngle.x, targetAngle.z) * Mathf.Rad2Deg;
        TF.rotation = Quaternion.Euler(0f, targetAngleY, 0f);
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngleY, 0f);
        TF.rotation = Quaternion.Slerp(TF.rotation, targetRotation, Time.deltaTime * 50f);
    }
    public void Shoot()
    {
        Bullet bullet = SimplePool.Spawn<Bullet>(TypeBullet, this.TF.position + new Vector3(0,1f,0), Quaternion.identity);
        if (bullet != null)
            bullet.Seek(target);
    }
}

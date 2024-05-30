using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    private Enemy target;
    public int damage = 25;
    public float speed = 70f;

    public void Seek(Enemy _target)
    {
        target = _target;
    }


    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    // Update is called once per frame
    void Update()
    {
         
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.TF.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget()
    {       
        if(target != null)
        {
            this.target.TakeDamage(damage);
        }
        
        OnDespawn();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitATTACK : UnitFSMState
{
    public float attackRange = 1.5f; 
    public float attackDelay = 1.0f; 

    private float lastAttackTime;

    void Start()
    {
        lastAttackTime = Time.time;
    }

    public override void BeginState()
    {
        if (manager.anim != null)
        {
            manager.anim.SetTrigger("AttackSpeed");
        }
        lastAttackTime = Time.time;
    }

    void Update()
    {
        if (manager.target != null)
        {
            float distance = Vector3.Distance(transform.position, manager.target.transform.position);

            if (distance < attackRange)
            {
                if (Time.time - lastAttackTime >= attackDelay)
                {
                    AttackTarget();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                manager.SetState(UnitState.MOVE);
            }
        }
        else
        {
            manager.SetState(UnitState.READY);
        }
    }

    private void AttackTarget()
    {
        if (manager.target != null)
        {
            UnitStat targetStat = manager.target.GetComponent<UnitStat>();
            if (targetStat != null)
            {
                bool isDead = targetStat.ApplyDamageReturnDead(manager.stat.power);
                if (isDead)
                {
                }
            }
        }
    }
}

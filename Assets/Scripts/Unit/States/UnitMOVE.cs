using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMOVE : UnitFSMState
{
    public override void BeginState()
    {
    }
    void Update()
    {
        if (manager.target != null)
        {
            if (manager.target.activeSelf == false)
            {
                manager.SetState(UnitState.READY);
                return;
            }
            if (Vector3.Distance(this.transform.position, manager.target.transform.position) <= manager.stat.attackRange)
            {
                manager.SetState(UnitState.ATTACK);
            }
            else
            {
                SRUtil.SRMove(manager.cc, this.transform, manager.target.transform.position, manager.stat.moveSpeed, manager.stat.rotateSpeed, manager.stat.fallSpeed);
            }
        }
        else
        {
            manager.SetState(UnitState.READY);
        }
    }
}
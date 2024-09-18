using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    IDLE=0,
    MOVE,
    ATTACK,
    DEAD,
    READY
}
public class UnitFSMState : MonoBehaviour
{
    protected UnitFSMManager manager;

    public virtual void BeginState()
    {
    }

    private void Awake()
    {
        manager = GetComponent<UnitFSMManager>();
    }
}
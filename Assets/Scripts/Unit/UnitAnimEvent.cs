using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimEvent : MonoBehaviour
{
    public UnitFSMManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<UnitFSMManager>();
    }

    void Attack()
    {
        manager.Attack();
    }
}
    
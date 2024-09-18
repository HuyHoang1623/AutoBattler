using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDEAD : UnitFSMState
{
    public override void BeginState()
    {
        gameObject.SetActive(false);
        return;
    }
    void Start() { }
}

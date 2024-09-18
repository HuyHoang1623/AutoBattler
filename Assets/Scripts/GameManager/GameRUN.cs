using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRUN : GameFSMState
{

    public Color backgroundColor;

    public override void BeginState()
    {
        base.BeginState();
        foreach (GameObject obj in manager.FindUnits())
        {
            UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
            if (unit != null)
            {
                unit.SetState(UnitState.READY);
            }
            else
            {
                Debug.LogError("Unit object is null. Cannot set state to READY.");
            }
            unit.stat.origin = unit.transform.position;
        }

        Camera.main.backgroundColor = backgroundColor;

    }
    void Start() { }

    void Update()
    {
        if (manager.FindEnemies().Count == 0)
        {
            manager.SetState(GameState.WIN);
        }
        else if (manager.FindAllies().Count == 0)
        {
            manager.SetState(GameState.LOSE);
        }
    }
}

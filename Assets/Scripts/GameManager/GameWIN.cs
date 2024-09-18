using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWIN : GameFSMState
{
    public int time = 5;
    private int timeLeft;

    public Color backgroundColor;


    public override void BeginState()
    {
        base.BeginState();

        timeLeft = time;
        manager.money += manager.stageManager.stages[manager.stageManager.currentStage].reward;

        manager.stageManager.currentStage++;

        DecreaseTime();

        foreach (GameObject obj in manager.FindEnemies())
        {
            UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
            unit.target = null;
            unit.SetState(UnitState.IDLE);
        }

        Camera.main.backgroundColor = backgroundColor;

    }
    void Start() { }

    public void DecreaseTime()
    {
        if (timeLeft > 0)
        {
            timeLeft--;
            Invoke("DecreaseTime", 1f);
        }
        else
        {
            timeLeft = time;
            manager.SetState(GameState.READY);
        }
    }
}

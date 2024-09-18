using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    READY = 0,
    RUN,
    WIN,
    LOSE
}

public class GameFSMState : MonoBehaviour
{
    protected GameFSMManager manager;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<GameFSMManager>();
        
    }
}

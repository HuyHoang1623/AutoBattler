using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    GameFSMManager manager;

    public GameObject HPBar;

    private Canvas canvas;

    public StatManager statManager;

    void Awake()
    {
        manager = GetComponentInParent<GameFSMManager>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
    void Start() 
    { 
        statManager.gameObject.SetActive(false); 
    }

    public Text timeLeftText;
    public Text moneyText;

    void Update()
    {
        moneyText.text = manager.money.ToString() + " Coins";
        if (manager.currentState == GameState.READY)
        {
            GameREADY ready = GetComponent<GameREADY>();
            timeLeftText.text = ready.timeLeft.ToString();  
        }
        else if (manager.currentState == GameState.LOSE)
        {
            timeLeftText.text = "Lose";
        }
        else if (manager.currentState == GameState.WIN)
        {
            timeLeftText.text = "Win";
        }
    }

    public void MakeHPBar(UnitStat target)
    {
        GameObject bar = Instantiate(HPBar);
        bar.transform.parent = canvas.transform;

        HPBarManager manager = bar.GetComponent<HPBarManager>();
        manager.stat = target;
        target.hpBar = manager;
    }
}

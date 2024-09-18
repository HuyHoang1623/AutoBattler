    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameREADY : GameFSMState
    {
        public int time = 15;
        public int timeLeft;

        public Color backgroundColor;
        public override void BeginState()
        {
            base.BeginState();
            timeLeft = time;
            DecreaseTime();



            foreach (GameObject obj in manager.enemies)
            {
                if (obj != null)
                {
                    Destroy(obj.GetComponent<UnitFSMManager>().stat.hpBar.gameObject);
                    Destroy(obj);
                }

            }

            foreach (GameObject obj in manager.allies)
            {
                UnitFSMManager unit = obj.GetComponent<UnitFSMManager>();
                unit.gameObject.SetActive(true);
                unit.stat.currentHp = unit.stat.hp;
            }

            manager.stageManager.CreateUnits(manager.stageManager.currentStage);
            manager.shop.Refresh();

            manager.allies = manager.FindAllies();
            manager.enemies = manager.FindEnemies();

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
                manager.SetState(GameState.RUN);
            }
        }
    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitREADY : UnitFSMState
{
    List<GameObject> gameObjects;

    public override void BeginState()
    {
        gameObjects = new List<GameObject>();
        if (manager.GetUnitType() == UnitType.ENEMY)
        {
            gameObjects.AddRange(GameFSMManager.instance.FindAllies());
        }
        else
        {
            gameObjects.AddRange(GameFSMManager.instance.FindEnemies());
        }

        gameObjects.Remove(this.gameObject);

        if (gameObjects.Count == 0)
        {
            manager.SetState(UnitState.IDLE);
        }
        else
        {
            float closestDist = Mathf.Infinity;
            GameObject closest = null;

            foreach (GameObject i in gameObjects)
            {
                Vector3 objectPos = i.transform.position;
                float dist = (objectPos - transform.position).sqrMagnitude;

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = i;
                }
            }
            manager.target = closest;
            manager.SetState(UnitState.MOVE);
        }
    }
    void Start() { }
}
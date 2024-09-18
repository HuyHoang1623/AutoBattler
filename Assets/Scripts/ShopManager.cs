using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public struct item
    {
        public GameObject obj;
        public UnitStat stat
        {
            get
            {
                return obj.GetComponent<UnitStat>();
            }
        }
        public Sprite image;
    }

    [SerializeField]
    public List<ShopManager.item> items = new List<ShopManager.item>();

    public ShopManager.item currentItem;
    void Awake() { Refresh(); }

    public void Refresh()
    {
        currentItem = items[Random.Range(0, items.Count)];
        GetComponentInChildren<Image>().sprite = currentItem.image;
    }

    public void Buy()
    {
        if (GameFSMManager.instance.currentState != GameState.READY) { return; }

        if (currentItem.stat == null)
        {
            Debug.LogError("currentItem.stat is null.");
            return;
        }

        if (currentItem.stat.price <= GameFSMManager.instance.money)
        {
            currentItem.stat.type = UnitType.ALLY;
            currentItem.stat.currentHp = currentItem.stat.hp;
            GameObject obj = Instantiate(currentItem.obj);
            obj.transform.position = Vector3.zero;

            GameFSMManager.instance.money -= currentItem.stat.price;

            CheckLevelup(obj);

            Refresh();
        }
        else
        {
            Debug.LogWarning("Not enough money to buy the item.");
        }
    }

    public void CheckLevelup(GameObject obj)
    {
        List<GameObject> levelupObjs = new List<GameObject>();
        foreach (GameObject o in GameFSMManager.instance.FindAllies())
        {
            if (o == obj) { continue; }
            UnitStat o_stat = o.GetComponent<UnitStat>();
            if (o_stat.name == currentItem.stat.name && o_stat.level == currentItem.stat.level)
            {
                levelupObjs.Add(o);
            }
        }
        if (levelupObjs.Count >= 2)
        {
            foreach (GameObject o in levelupObjs)
            {
                Destroy(o.GetComponent<UnitStat>().hpBar.gameObject);
                Destroy(o.gameObject);
                o.GetComponent<UnitFSMManager>().SetState(UnitState.DEAD);
            }
            obj.GetComponent<UnitStat>().level++;
        }
    }

    public void mouseEnter()
    {
        if (currentItem.stat != null)
        {
            currentItem.stat.transform.position = this.transform.position;
            GameFSMManager.instance.ui.statManager.Shop_Goto(currentItem.stat);
        }
    }

    public void mouseExit()
    {
        GameFSMManager.instance.ui.statManager.Exit();
    }

    public void ResetShop()
    {
        if (GameFSMManager.instance.money >= 2)
        {
            GameFSMManager.instance.money -= 2;
            Refresh();
        }
        else
        {
            Debug.LogWarning("Not enough money to reset the shop.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public UnitStat stat;
    public Text info;

    public void Goto(UnitStat stat)
    {
        this.gameObject.SetActive(true);
        info.text = stat.name+" "+(stat.level+1).ToString()+" level"+
                    "\nAttack Speed: "+stat.attackSpeed.ToString()+
                    "\nPower: "+ stat.power.ToString()+
                    "\nMove Speed: "+ stat.moveSpeed.ToString();
        Vector3 pos = stat.transform.position;
        transform.position = Camera.main.WorldToScreenPoint(pos);
    }

    public void Shop_Goto(UnitStat stat)
    {
        this.gameObject.SetActive(true);
        info.text = stat.name +
                    "\nAttack Speed: " + stat.attackSpeed.ToString() +
                    "\nPower: " + stat.power.ToString() +
                    "\nMove Speed:" + stat.moveSpeed.ToString() +
                    "\nPrice: " + stat.price.ToString();
        Vector3 pos = stat.transform.position;
        transform.position = pos;
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}

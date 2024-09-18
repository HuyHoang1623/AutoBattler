using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    public UnitStat stat;
    private Image backGround;
    private Image foreGround;


    void Awake()
    {
        backGround = transform.GetChild(0).GetComponent<Image>();
        foreGround = transform.GetChild(1).GetComponent<Image>();
    }

    void Start()
    {

    }

    void Update()
    {
        foreGround.rectTransform.localScale = new Vector3(stat.currentHp / stat.hp, 1, 1);
    }

    void LateUpdate()
    {
        if (stat != null)
        {
            if (stat.gameObject.activeSelf == false)
            {
                backGround.enabled = false;
                foreGround.enabled = false;
            }
            else
            {
                backGround.enabled = true;
                foreGround.enabled = true;
            }
            Vector3 healthBarWorldPosition = stat.transform.position + new Vector3(0.0f, 6f, 0.0f);
            transform.position = Camera.main.WorldToScreenPoint(healthBarWorldPosition);
        }

    }
}

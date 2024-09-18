using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStat : MonoBehaviour
{
    public UnitType type;
    [SerializeField]
    private int _level;

    public int level
    {
        get
        {
            return _level;
        }
        set
        {
            ;
            if (value > _level)
            {
                float weight = 1 + 0.2f * value;
                GetComponent<Transform>().localScale = new Vector3(weight, weight, weight);
                moveSpeed *= weight;
                power *= weight;
                attackSpeed *= weight;
            }
            _level = value;

        }
    }
    public string name;
    public float moveSpeed;
    public float fallSpeed;
    public float rotateSpeed;
    public float attackRange;
    public float power;
    public int price;

    [SerializeField]
    private float _attackSpeed = 1;


    public float attackSpeed
    {
        get
        {
            return _attackSpeed;
        }
        set
        {
            _attackSpeed = value;
            if (manager != null)
            {
                manager.anim.SetFloat("AttackSpeed", value);
            }
        }
    }

    public float hp;
    public float currentHp;

    public UnitFSMManager manager;

    public Vector3 origin;

    public HPBarManager hpBar;

    private void Awake()
    {
        manager = GetComponent<UnitFSMManager>();
    }

    void Start()
    {
        manager.anim.SetFloat("AttackSpeed", attackSpeed);
        level = level;
    }

    public bool ApplyDamageReturnDead(float amount)
    {
        currentHp -= amount;
        Debug.Log("[" + name + "] took damage: " + amount);
        if (currentHp <= 0)
        {
            manager.Dead();
            return true;
        }
        return false;
    }
}
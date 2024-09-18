using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum UnitType
{
    NONE = 0,
    ALLY,
    ENEMY
}
    
public class UnitFSMManager : MonoBehaviour
{
    public UnitState currentState;
    public UnitState startState;

    Dictionary<UnitState, UnitFSMState> states = new Dictionary<UnitState, UnitFSMState>();
    public static UnitFSMManager instance;

    public CharacterController cc;
    public static GameObject itemBeingDragged;
    Vector3 startPosition;

    public GameObject target;
    public Animator anim;
    public UnitStat stat;

    public UnitType GetUnitType()
    {
        if (gameObject.CompareTag("ENEMY"))
        {
            return UnitType.ENEMY;
        }
        else if (gameObject.CompareTag("ALLY"))
        {
            return UnitType.ALLY;
        }
        return UnitType.NONE;
    }

    void Awake()
    {
        instance = this;
        states.Add(UnitState.IDLE, GetComponent<UnitIDLE>());
        states.Add(UnitState.MOVE, GetComponent<UnitMOVE>());
        states.Add(UnitState.ATTACK, GetComponent<UnitATTACK>());
        states.Add(UnitState.DEAD, GetComponent<UnitDEAD>());
        states.Add(UnitState.READY, GetComponent<UnitREADY>());

        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        stat = GetComponent<UnitStat>();
        tag = stat.type.ToString();
    }

    void Start()
    {
        SetState(startState);
        GameFSMManager.instance.ui.MakeHPBar(stat);
    }

    public void SetState(UnitState newState)
    {
        foreach (UnitFSMState fsm in states.Values)
        {
            if (fsm != null)
            {
                fsm.enabled = false;
            }
            else
            {
                Debug.LogWarning($"Null state found for {newState}. Please check the initialization.");
            }
        }

        currentState = newState;
        states[newState].enabled = true;
        states[newState].BeginState();
        anim.SetInteger("CurrentState", (int)currentState);
    }

    void Update()
    {
        if (target != null && !target.activeSelf)
        {
            target = null;
        }
    }

    Vector3 dist;
    Vector3 startPos;
    float posX, posY, posZ;

    void OnMouseDown()
    {
        if (GetUnitType() == UnitType.ENEMY || GameFSMManager.instance.currentState == GameState.RUN)
        {
            return;
        }

        startPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseOver()
    {
        GameFSMManager.instance.ui.statManager.Goto(stat);
    }

    void OnMouseExit()
    {
        GameFSMManager.instance.ui.statManager.Exit();
    }

    void OnMouseDrag()
    {
        if (GetUnitType() == UnitType.ENEMY || GameFSMManager.instance.currentState == GameState.RUN)
        {
            return;
        }

        float planeY = 0;
        Transform draggingObject = transform;

        Plane plane = new Plane(Vector3.up, Vector3.up * planeY);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            draggingObject.position = ray.GetPoint(distance);
        }
    }

    public void Attack()
    {
        if (target != null)
        {
            bool targetDead = target.GetComponent<UnitStat>().ApplyDamageReturnDead(stat.power);
            if (targetDead)
            {
                target = null;
            }
        }

        SetState(UnitState.READY);  
    }

    public void Dead()
    {
        SetState(UnitState.DEAD);
    }
}

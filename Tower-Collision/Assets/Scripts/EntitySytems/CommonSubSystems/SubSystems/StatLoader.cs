using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


public struct AttackStats
{
    public float Attack { get; private set; }
    public float AttackRange { get; private set; }
    public float DetectRange { get; private set; }
    public Elements Element { get; private set; }
    public float AtkInterval { get; private set; }
    public TargetPriority Targeting { get; private set; }
    public AttackAction AttackAction { get; private set; }
    public LayerMask TargetLayer { get; private set; }

    public AttackStats(float attack, float attackRange,float detectRange, Elements element, float atkInterval, TargetPriority targeting, AttackAction attackAction,LayerMask targetLayer)
    {
        Attack = attack;
        AttackRange = detectRange;
        DetectRange = attackRange;
        Element = element;
        AtkInterval = atkInterval;
        Targeting = targeting;
        AttackAction = attackAction;
        TargetLayer = targetLayer;
    }
}

public struct HealthStats
{
    public float Health { get; private set; }
    public float Defense { get; private set; }
    public float BreakAmount { get; private set; }

    public HealthStats(float healthValue, float defenseValue, float breakAmount)
    {
        Health = healthValue;
        Defense = defenseValue;
        BreakAmount = breakAmount;
    }
}

public abstract class StatLoader : MonoBehaviour
{
    public abstract AttackStats AttackLoader();
    public abstract HealthStats HealthLoader();
    public abstract List<State> StateLoader();
}

using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


public struct AttackStats
{
    public float Attack { get; private set; }
    public DamageTypes DamageType { get; private set; }
    public float Ticks { get; private set; }
    public SameType SameType { get; private set; }
    public Yellow Yellow { get; private set; }
    public Cyan Cyan { get; private set; }
    public Magenta Magenta { get; private set; }
    public float AttackRange { get; private set; }
    public float DetectRange { get; private set; }
    public float AtkInterval { get; private set; }
    public TargetPriority Targeting { get; private set; }
    public AttackAction AttackAction { get; private set; }
    public LayerMask TargetLayer { get; private set; }

    // Constructor
    public AttackStats(float attack, DamageTypes damageType, float ticks, SameType sameType, Yellow yellow, Cyan cyan, Magenta magenta,
                       float attackRange, float detectRange, float atkInterval, TargetPriority targeting, AttackAction attackAction, LayerMask targetLayer)
    {
        Attack = attack;
        DamageType = damageType;
        Ticks = ticks;
        SameType = sameType;
        Yellow = yellow;
        Cyan = cyan;
        Magenta = magenta;
        AttackRange = attackRange;
        DetectRange = detectRange;
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

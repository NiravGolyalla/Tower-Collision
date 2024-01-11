using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonStats : ScriptableObject
{
    public float Health;
    public float Attack;
    public float Defense;
    public float DetectRange;
    public float AttackRange;
    public DamageTypes DamageType;
    public float BreakAmount;
    public float BreakInterval;
    public float AtkInterval;
    public TargetPriority Targeting;
    public AttackAction AttackAction;
    public LayerMask TargetLayer;
    public SameType sameType;
    public Cyan cyan;
    public Magenta magenta;
    public Yellow yellow;
    public float ticks;


}

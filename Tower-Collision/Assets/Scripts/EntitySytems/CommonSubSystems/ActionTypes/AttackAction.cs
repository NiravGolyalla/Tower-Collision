using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public abstract class AttackAction : ScriptableObject
{
    public abstract void PerformAttack(AttackSubSystem attacker ,StateMachine target);
}

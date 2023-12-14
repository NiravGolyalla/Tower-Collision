using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerAttackState", menuName = "States/TowerAttackState")]
public class TowerAttackState : State
{
    public override State UpdateState(StateMachine system)
    {
        TowerSystem towersystem = (TowerSystem)system;
        if(towersystem.attackSubSystem.currTarget == null || Vector3.Distance(towersystem.attackSubSystem.currTarget.transform.position,towersystem.transform.position) > towersystem.attackSubSystem.DetectRange){
            Debug.Log("Here");
            Debug.Log("Here");
            return SwitchState(towersystem,towersystem.searchState);
        }
        towersystem.attackSubSystem.ManageCooldown();
        towersystem.attackSubSystem.AttackTarget();
        return this;
    }

    public override void ResetStateFlags(StateMachine system)
    {
        system.attackSubSystem.wipeTarget();
        base.ResetStateFlags(system);
    }
}

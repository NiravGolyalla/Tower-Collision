using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TowerSearchState", menuName = "States/TowerSearchState")]
public class TowerSearchState : State
{
    public override State UpdateState(StateMachine system)
    {
        TowerSystem towersystem = (TowerSystem)system;
        towersystem.attackSubSystem.DetectTarget();
        towersystem.attackSubSystem.ManageCooldown();
        if(towersystem.attackSubSystem.currTarget != null){
            return SwitchState(towersystem,towersystem.attackState);
        }
        return this;
    }
}

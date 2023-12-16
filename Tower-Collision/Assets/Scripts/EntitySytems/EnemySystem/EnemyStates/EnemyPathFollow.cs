using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPathFollow", menuName = "States/Enemy/EnemyPathFollow")]
public class EnemyPathFollow : State
{
    public override State UpdateState(StateMachine system)
    {
        Debug.Log("EnemyPathFollow");
        EnemySystem enemySystem = (EnemySystem)system;
        enemySystem.movementSubSystem.FollowPath();
        enemySystem.movementSubSystem.MoveToTarget(enemySystem.movementSubSystem.target);
        enemySystem.attackSubSystem.DetectTarget();
        Debug.Log(enemySystem.attackSubSystem.currTarget);
        if(enemySystem.attackSubSystem.currTarget != null){
            ((TowerSystem)enemySystem.attackSubSystem.currTarget).blockerSubSystem.BlockUnit(enemySystem);
            return SwitchState(enemySystem,enemySystem.enemyApproachState);
        }
        
        return this;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPathFollow", menuName = "States/Enemy/EnemyPathFollow")]
public class EnemyPathFollow : State
{
    public override State UpdateState(StateMachine system)
    {
    
        EnemySystem enemySystem = (EnemySystem)system;
        enemySystem.state = "EnemyPathFollow";
        enemySystem.movementSubSystem.agent.isStopped = false;

        if(enemySystem.healthSubSystem.BreakPercent == 0){
            return SwitchState(enemySystem,enemySystem.enemyBroken);
        }
        enemySystem.attackSubSystem.DetectTarget();
        if(Vector3.Distance(enemySystem.movementSubSystem.target.position,enemySystem.movementSubSystem.agent.destination) > 1f){
            enemySystem.movementSubSystem.MoveToTarget(enemySystem.movementSubSystem.target);
            enemySystem.movementSubSystem.agent.stoppingDistance = 0f;
        }
        
        enemySystem.movementSubSystem.FollowPath();
        
        if(enemySystem.attackSubSystem.currTarget != null ){
            // ((TowerSystem)enemySystem.attackSubSystem.currTarget).blockerSubSystem.BlockUnit(enemySystem);;
            return SwitchState(enemySystem,enemySystem.enemyApproachState);
        }
        
        return this;
    }
}

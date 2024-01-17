using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyApproachState", menuName = "States/Enemy/EnemyApproachState")]
public class EnemyApproachState : State
{
    public override State UpdateState(StateMachine system)
    {
        EnemySystem enemySystem = (EnemySystem)system;
        enemySystem.state = "EnemyApproachState";
        enemySystem.movementSubSystem.agent.isStopped = false;
        if(enemySystem.healthSubSystem.BreakPercent == 0){
            return SwitchState(enemySystem,enemySystem.enemyBroken);
        }
        if(enemySystem.attackSubSystem.currTarget == null){
            return SwitchState(enemySystem,enemySystem.enemyPathFollowState);
        }
        if(Vector3.Distance(enemySystem.attackSubSystem.currTarget.transform.position,enemySystem.movementSubSystem.agent.destination) > 1f){
            enemySystem.movementSubSystem.MoveToTarget(enemySystem.attackSubSystem.currTarget.transform);
            enemySystem.movementSubSystem.agent.stoppingDistance = enemySystem.attackSubSystem.AttackRange/2;
        }
        float distance = Vector3.Distance(enemySystem.attackSubSystem.currTarget.transform.position,enemySystem.transform.position); 
        if(distance >= enemySystem.attackSubSystem.DetectRange){
            return SwitchState(enemySystem,enemySystem.enemyPathFollowState);
        }
        if(distance < enemySystem.attackSubSystem.AttackRange){
            return SwitchState(enemySystem,enemySystem.enemyAttackState);
        }
        
        
        return this;
    }

    public override void ResetStateFlags(StateMachine system)
    {
        base.ResetStateFlags(system);
    }
}

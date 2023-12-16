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
        enemySystem.movementSubSystem.MoveToTarget(enemySystem.attackSubSystem.currTarget.transform);
        Debug.Log("Approch");
        if(enemySystem.attackSubSystem.currTarget == null){
            return SwitchState(enemySystem,enemySystem.enemyPathFollowState);
        }
        float distance = Vector3.Distance(enemySystem.attackSubSystem.currTarget.transform.position,enemySystem.transform.position); 
        if(distance > enemySystem.attackSubSystem.DetectRange){
            return SwitchState(enemySystem,enemySystem.enemyPathFollowState);
        }
        if(distance < enemySystem.attackSubSystem.AttackRange){
            return SwitchState(enemySystem,enemySystem.enemyAttackState);
        }
        Debug.Log("HEre");
        
        return this;
    }

    public override void ResetStateFlags(StateMachine system)
    {
        base.ResetStateFlags(system);
    }
}

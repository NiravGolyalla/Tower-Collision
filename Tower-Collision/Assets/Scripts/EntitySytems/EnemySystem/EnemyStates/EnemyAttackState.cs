using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackState", menuName = "States/Enemy/EnemyAttackState")]
public class EnemyAttackState : State
{
    public override State UpdateState(StateMachine system)
    {
        Debug.Log("AttackState");
        EnemySystem enemySystem = (EnemySystem)system;
        if(enemySystem.attackSubSystem.currTarget == null){
            return SwitchState(enemySystem,enemySystem.enemyPathFollowState);
        }
        float distance = Vector3.Distance(enemySystem.attackSubSystem.currTarget.transform.position,enemySystem.transform.position); 
        if(distance > enemySystem.attackSubSystem.AttackRange){
            return SwitchState(enemySystem,enemySystem.enemyApproachState);
        }
        enemySystem.attackSubSystem.AttackTarget();
        return this;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBroken", menuName = "States/Enemy/EnemyBroken")]
public class EnemyBroken : State
{
    float multiplier = .005f;
    float increase = 0f;
    float delay = 1f;
    public override State UpdateState(StateMachine system)
    {
        EnemySystem enemySystem = (EnemySystem)system;
        // Debug.Log("Broken");
        enemySystem.movementSubSystem.agent.isStopped = true;
        if(delay <= 0f){
            enemySystem.healthSubSystem.RegenBreak(Mathf.Pow(increase,2)*(multiplier)/2);
            increase += Time.deltaTime;
        } else{
            delay -= Time.deltaTime;
        }

        if(enemySystem.healthSubSystem.BreakPercent == 1){
            return SwitchState(enemySystem,enemySystem.enemyPathFollowState);
        }
        return this;
    }

    public override void ResetStateFlags(StateMachine system)
    {
        increase = 1;
        delay = 1;
    }
}

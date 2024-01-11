using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EnemySystem : StateMachine
{
    public AIMovementSubSystem movementSubSystem{get;private set;}
    public EnemyPathFollow enemyPathFollowState{get;private set;}
    public EnemyApproachState enemyApproachState{get;private set;}
    public EnemyAttackState enemyAttackState{get;private set;}
    public string state;

    protected override void Awake(){
        base.Awake();
        statLoader = GetComponent<EnemyStatsLoader>();
        attackSubSystem = GetComponent<AttackSubSystem>();
        healthSubSystem = GetComponent<HealthSubSystem>();
        movementSubSystem = GetComponent<AIMovementSubSystem>();
    }

    public override void StartSystem(){
        List<State> states = statLoader.StateLoader();
        enemyPathFollowState = (EnemyPathFollow)states[0];
        enemyApproachState = (EnemyApproachState)states[1];
        enemyAttackState = (EnemyAttackState)states[2];
        currentState = enemyPathFollowState;

        attackSubSystem.GetStats();
        healthSubSystem.GetStats();
        movementSubSystem.GetStats();
        
        enableMachine = true;
    }

}

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

    protected virtual void Awake(){
        statLoader = GetComponent<EnemyStatsLoader>();
        attackSubSystem = GetComponent<AttackSubSystem>();
        healthSubSystem = GetComponent<HealthSubSystem>();
        movementSubSystem = GetComponent<AIMovementSubSystem>();
        
        List<State> states = statLoader.StateLoader();
        enemyPathFollowState = (EnemyPathFollow)states[0];
        enemyApproachState = (EnemyApproachState)states[1];
        enemyAttackState = (EnemyAttackState)states[2];
        currentState = enemyPathFollowState;
    }

    void Update(){
        StateUpdate();
    }
}

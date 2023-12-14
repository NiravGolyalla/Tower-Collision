using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TowerSystem : StateMachine
{
    public TowerSearchState searchState{get; private set;}
    public TowerAttackState attackState{get; private set;}

    protected virtual void Awake(){
        statLoader = GetComponent<TowerStatsLoader>();
        attackSubSystem = GetComponent<AttackSubSystem>();
        healthSubSystem = GetComponent<HealthSubSystem>();
        
        List<State> states = statLoader.StateLoader();
        searchState = (TowerSearchState)states[0];
        attackState = (TowerAttackState)states[1];
        currentState = searchState;
    }

    void Update(){
        StateUpdate();
    }
}
    
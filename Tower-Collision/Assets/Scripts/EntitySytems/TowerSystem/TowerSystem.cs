using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TowerSystem : StateMachine
{
    public TowerSearchState searchState{get; private set;}
    public TowerAttackState attackState{get; private set;}
    public BlockerSubSystem blockerSubSystem{get; private set;}

    protected override void Awake(){
        base.Awake();
        statLoader = GetComponent<TowerStatsLoader>();
        attackSubSystem = GetComponent<AttackSubSystem>();
        healthSubSystem = GetComponent<HealthSubSystem>();
        // blockerSubSystem = GetComponent<BlockerSubSystem>();
    }

    public override void StartSystem(){
        List<State> states = statLoader.StateLoader();
        searchState = (TowerSearchState)states[0];
        attackState = (TowerAttackState)states[1];
        currentState = searchState;

        attackSubSystem.GetStats();
        healthSubSystem.GetStats();
        // blockerSubSystem.GetStats();        

        enableMachine = true;
    }
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TowerSystem : StateMachine
{
    public TowerSearchState searchState{get; private set;}
    public TowerAttackState attackState{get; private set;}
    public BlockerSubSystem blockerSubSystem{get; private set;}

    void Awake(){
        statLoader = GetComponent<TowerStatsLoader>();
        attackSubSystem = GetComponent<AttackSubSystem>();
        healthSubSystem = GetComponent<HealthSubSystem>();
        // blockerSubSystem = GetComponent<BlockerSubSystem>();
    }

    void Start(){
        List<State> states = statLoader.StateLoader();
        searchState = (TowerSearchState)states[0];
        attackState = (TowerAttackState)states[1];
        currentState = searchState;
    }

    public override void Death()
    {
        base.Death();
    }
}
    
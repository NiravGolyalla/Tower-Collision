using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatsLoader : StatLoader
{
    [SerializeField]TowerStats stats;

    public override AttackStats AttackLoader(){
        
        return new AttackStats(stats.Attack,TowerColorSelect.clickedValue,stats.ticks,stats.sameType,stats.yellow,stats.cyan,stats.magenta,stats.AttackRange,stats.DetectRange,stats.AtkInterval,stats.Targeting,stats.AttackAction,stats.TargetLayer);
    }

    public override HealthStats HealthLoader()
    {
        return new HealthStats(stats.Health,stats.Defense,stats.BreakAmount);
    }

    public virtual float GetBlockAmount(){
        return stats.blockAmount;
    }

    public override List<State> StateLoader()
    {
        List<State> states = new List<State>
        {
            stats.SearchState,
            stats.AttackState
        };
        return states; 
    }

}


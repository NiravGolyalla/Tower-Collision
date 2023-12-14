using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsLoader : StatLoader
{
    [SerializeField]EnemyStats stats;

    public override AttackStats AttackLoader(){
        return new AttackStats(stats.Attack,stats.AttackRange,stats.DetectRange,stats.Element,stats.AtkInterval,stats.Targeting,stats.AttackAction,stats.TargetLayer);
    }

    public override HealthStats HealthLoader()
    {
        return new HealthStats(stats.Health,stats.Defense,stats.BreakAmount);
    }

    public virtual float getSpeed(){
        return stats.speed;
    }

    public override List<State> StateLoader()
    {
        return null;
    }
}


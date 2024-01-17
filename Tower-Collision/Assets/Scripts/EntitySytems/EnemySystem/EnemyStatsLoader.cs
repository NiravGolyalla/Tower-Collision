using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStatsLoader : StatLoader
{
    [SerializeField]EnemyStats stats;

    public void SetStats(EnemyStats enemyStats)
    {
        stats = enemyStats;
    }

    public override AttackStats AttackLoader(){
        DamageTypes dmgType = stats.DamageType;
        if(stats.randomElement){
            int i = Random.Range(0,3);
            dmgType = (DamageTypes)i;
        }
        return new AttackStats(stats.Attack,dmgType,stats.ticks,stats.sameType,stats.yellow,stats.cyan,stats.magenta,stats.AttackRange,stats.DetectRange,stats.AtkInterval,stats.Targeting,stats.AttackAction,stats.TargetLayer);
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
        List<State> states = new List<State>
        {
            stats.enemyPathFollowState,
            stats.enemyApproachState,
            stats.enemyAttackState,
            stats.enemyBroken
        };
        return states; 
    }
}


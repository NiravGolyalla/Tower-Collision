using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


[CreateAssetMenu(fileName = "EnemyFiring", menuName = "Custom/Create EnemyFiring")]
public class EnemyFiring : TargetPriority
{
    public override StateMachine DetermineTarget(List<StateMachine> targets){
        return targets[0];
    }

    
}

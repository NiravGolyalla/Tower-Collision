using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


[CreateAssetMenu(fileName = "TowerClosestToExit", menuName = "Custom/Create TowerClosestToExit")]
public class TowerClosestToExit : TargetPriority
{
    public override StateMachine DetermineTarget(List<StateMachine> targets){
        (float,int) closestToExit = (float.MaxValue,-1);
        for(int i = 0; i < targets.Count;i++){
            if(targets[i] == null){
                continue;
            }
            if(closestToExit.Item1 > ((EnemySystem)targets[i]).distanceFromExit){
                closestToExit = (((EnemySystem)targets[i]).distanceFromExit,i);
            }
        }
        return (closestToExit.Item2 != -1) ? targets[closestToExit.Item2] : null;   
    }

    
}

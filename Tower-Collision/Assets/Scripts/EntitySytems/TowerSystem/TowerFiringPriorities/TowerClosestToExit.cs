using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(fileName = "TowerClosestToExit", menuName = "Action/DetectTarget/Create TowerClosestToExit")]
public class TowerClosestToExit : TargetPriority
{
    public override StateMachine DetermineTarget(StateMachine system, List<StateMachine> targets){
        (float,int) closestToExit = (float.MaxValue,-1);
        for(int i = 0; i < targets.Count;i++){
            if(targets[i] == null){
                continue;
            }
            if(closestToExit.Item1 > ((EnemySystem)targets[i]).movementSubSystem.distanceFromExit){
                closestToExit = (((EnemySystem)targets[i]).movementSubSystem.distanceFromExit,i);
            }
        }
        return (closestToExit.Item2 != -1) ? targets[closestToExit.Item2] : null;   
    }    
}

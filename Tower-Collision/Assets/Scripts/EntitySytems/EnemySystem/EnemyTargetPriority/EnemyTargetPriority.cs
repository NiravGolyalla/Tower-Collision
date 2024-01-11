using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(fileName = "EnemyTargetPriority", menuName = "Action/DetectTarget/Create EnemyTargetPriority")]
public class EnemyTargetPriority : TargetPriority
{
    public override StateMachine DetermineTarget(StateMachine system, List<StateMachine> targets){
        EnemySystem enemySystem = (EnemySystem)system;
        (float, int) closestToExit = (float.MaxValue, -1);
        for (int i = 0; i < targets.Count; i++){
            if (targets[i] == null){
                continue;
            }
            float distance = Vector3.Distance(targets[i].transform.position,enemySystem.transform.position);
            if (closestToExit.Item1 > distance){
                if (targets[i].GetType() == typeof(TowerSystem)){
                    closestToExit = (distance, i);
                    // if(((TowerSystem)targets[i]).blockerSubSystem.checkBlockStatus()){
                        
                    // }
                }
                
            }
        }
        return (closestToExit.Item2 != -1) ? targets[closestToExit.Item2] : null;
    }
}

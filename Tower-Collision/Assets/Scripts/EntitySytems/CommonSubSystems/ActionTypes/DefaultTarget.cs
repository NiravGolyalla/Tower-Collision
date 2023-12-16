using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


[CreateAssetMenu(fileName = "DefaultTarget", menuName = "Action/DetectTarget/Create DefaultTarget")]
public class DefaultTarget : TargetPriority
{
    public override StateMachine DetermineTarget(StateMachine system, List<StateMachine> targets){
        (float, int) closestToExit = (float.MaxValue, -1);
        for (int i = 0; i < targets.Count; i++){
            if (targets[i] == null){
                continue;
            }
            float distance = Vector3.Distance(targets[i].transform.position,system.transform.position);
            if (closestToExit.Item1 > distance){
                closestToExit = (distance, i);
            }
        }
        return (closestToExit.Item2 != -1) ? targets[closestToExit.Item2] : null;
    }
}

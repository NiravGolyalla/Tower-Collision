using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EnemySystem : StateMachine
{
    public List<Transform> path{get;set;} 
    [SerializeField]int index = 0;
    public Transform target{get;set;}
    public float distanceFromExit = 10f;
    AIMovementSubSystem movementSubSystem;

    protected virtual void Awake(){
        statLoader = GetComponent<EnemyStatsLoader>();
        attackSubSystem = GetComponent<AttackSubSystem>();
        healthSubSystem = GetComponent<HealthSubSystem>();
        movementSubSystem = GetComponent<AIMovementSubSystem>();
    }

    void Update(){
        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            index += 1;
            if (index >= path.Count){
                Destroy(gameObject);
                return;
            }
            target = path[index];
        }
        distanceFromExit = (path.Count - index)*Vector3.Distance(transform.position, target.position);
        movementSubSystem.MoveToTarget(target);
        // attackSubSystem.DetectTarget();
        attackSubSystem.ManageCooldown();
        // attackSubSystem.AttackTarget();
    }
}

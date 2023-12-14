using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementSubSystem : MonoBehaviour
{
    StateMachine mainSystem;
    private NavMeshAgent agent;
    [SerializeField]float speed;
    
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        mainSystem = GetComponent<StateMachine>();
    }

    void Start(){
        speed = ((EnemyStatsLoader)mainSystem.statLoader).getSpeed();
    }

    public void MoveToTarget(Transform destination){
        agent.SetDestination(destination.position);
    }
}

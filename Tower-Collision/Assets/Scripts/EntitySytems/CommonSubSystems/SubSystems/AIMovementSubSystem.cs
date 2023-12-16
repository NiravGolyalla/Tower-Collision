using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementSubSystem : MonoBehaviour
{
    StateMachine mainSystem;
    private NavMeshAgent agent;
    [SerializeField]float speed;
    public List<Transform> path{get;private set;} 
    [SerializeField]int index = 0;
    public Transform target{get;private set;}
    public float distanceFromExit = 10f;
    
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        mainSystem = GetComponent<StateMachine>();
    }

    void Start(){
        speed = ((EnemyStatsLoader)mainSystem.statLoader).getSpeed();
        agent.speed = speed;
    }

    public void setPath(List<Transform> _path){
        path = _path;
        target = path[index];
    }

    public void MoveToTarget(Transform destination){
        agent.SetDestination(destination.position);
    }

    public void FollowPath(){
        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            index += 1;
            if (index >= path.Count){
                Destroy(gameObject);
                return;
            }
            target = path[index];
        }
    }
    void Update(){
        distanceFromExit = (path.Count - index)*Vector3.Distance(transform.position, target.position);
    }
}

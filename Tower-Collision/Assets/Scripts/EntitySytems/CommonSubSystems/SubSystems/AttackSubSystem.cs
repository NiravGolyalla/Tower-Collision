using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSubSystem : MonoBehaviour
{
    public float Attack {get; private set;}
    public Elements Element {get; private set;}
    public float AttackRange{get; private set;}
    public float DetectRange{get; private set;}
    float AtkInterval;
    TargetPriority Targeting;
    AttackAction AttackAction;
    LayerMask TargetLayer;

    private StateMachine mainSystem;
    public StateMachine currTarget {get; private set;}
    private float AtkCooldown = 0f;
    public Projectile projectile = null;
    [SerializeField]public Transform firingPoint;
    
    [SerializeField]private List<StateMachine> targets;
    void Awake(){
        mainSystem = GetComponent<StateMachine>();
    }

    void Start(){
        LoadAttackStats(mainSystem.statLoader.AttackLoader());
    }

    void Update(){
        if(tag != "Enemy"){
            print(currTarget);    
        }
        
    }

    public void LoadAttackStats(AttackStats existingStats)
    {
        Attack = existingStats.Attack;
        AttackRange = existingStats.AttackRange;
        DetectRange = existingStats.DetectRange;
        Element = existingStats.Element;
        AtkInterval = existingStats.AtkInterval;
        Targeting = existingStats.Targeting;
        AttackAction = existingStats.AttackAction;
        TargetLayer = existingStats.TargetLayer;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DetectRange);
    }

    public void DetectTarget(){
        if(currTarget != null){
            print("OMG YOU MMARTG");
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, DetectRange,TargetLayer);
        targets = new List<StateMachine>();
        foreach (Collider collider in colliders)
        {
            StateMachine target = collider.transform.GetComponent<StateMachine>();
            if(target != null){
                targets.Add(target);
            }
        }

        if(currTarget == null && targets.Count > 0){
            currTarget = Targeting.DetermineTarget(targets);
        }
    }

    public void ManageCooldown(){
        if(AtkCooldown > 0){
            AtkCooldown -= Time.deltaTime;
        }
    }

    public void AttackTarget(){
        if(currTarget != null && AtkCooldown <= 0 && Vector3.Distance(currTarget.transform.position,transform.position) < AttackRange){
            AtkCooldown = AtkInterval;
            AttackAction.PerformAttack(this,currTarget);
        }
    }

    public void wipeTarget(){
        if(Vector3.Distance(currTarget.transform.position,transform.position) > DetectRange){
            currTarget = null;
        }
    }
}

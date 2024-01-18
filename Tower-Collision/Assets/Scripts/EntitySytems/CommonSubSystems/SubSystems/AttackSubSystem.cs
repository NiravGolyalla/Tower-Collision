using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AttackSubSystem : MonoBehaviour
{
    public float Attack { get; private set; }
    public DamageTypes DamageType { get; private set; }
    public float Ticks { get; private set; }
    public SameType SameType { get; private set; }
    public Yellow Yellow { get; private set; }
    public Cyan Cyan { get; private set; }
    public Magenta Magenta { get; private set; }
    public float AttackRange { get; private set; }
    public float DetectRange { get; private set; }
    public float AtkInterval { get; private set; }
    public TargetPriority Targeting { get; private set; }
    public AttackAction AttackAction { get; private set; }
    public LayerMask TargetLayer { get; private set; }
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
        ManageCooldown();
    }

    public void LoadAttackStats(AttackStats other)
    {
        // Copy the values from the other AttackStats object
        Attack = other.Attack;
        DamageType = other.DamageType;
        Ticks = other.Ticks;
        SameType = other.SameType;
        Yellow = other.Yellow;
        Cyan = other.Cyan;
        Magenta = other.Magenta;
        AttackRange = other.AttackRange;
        DetectRange = other.DetectRange;
        AtkInterval = other.AtkInterval;
        Targeting = other.Targeting;
        AttackAction = other.AttackAction;
        TargetLayer = other.TargetLayer;
        firingPoint.GetComponent<Renderer>().material.color = Damage.colorMap[other.DamageType];
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public void DetectTarget(){
        if(currTarget != null){
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
            currTarget = Targeting.DetermineTarget(mainSystem,targets);
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
        if(currTarget == null){
            return;
        }
        if(Vector3.Distance(currTarget.transform.position,transform.position) > DetectRange){
            currTarget = null;
        }
    }
}

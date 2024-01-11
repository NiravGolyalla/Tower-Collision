using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;

public class HealthSubSystem : MonoBehaviour
{
    private StateMachine mainSystem;
    public float HealthPercent{get;private set;}
    public float BreakPercent{get;private set;}
    private float Health;
    private float MaxHealth;
    private float BreakAmount;
    private float MaxBreakAmount;
    private float Defense;
    private DamageTypes applied = DamageTypes.Null;
    private LinkedList<Damage> DamageInputs = new LinkedList<Damage>();

    public event Action<float,float,DamageTypes> onHealthChange;

    void Awake(){
        mainSystem = GetComponent<StateMachine>();
    }

    public void GetStats(){
        LoadHealthStats(mainSystem.statLoader.HealthLoader());
    }

    void Update(){
        if(mainSystem.enableMachine){
            ProcessDamage();
        }
    }

    public void LoadHealthStats(HealthStats other)
    {
        Health = other.Health;
        MaxHealth = other.Health;
        Defense = other.Defense;
        BreakAmount = other.BreakAmount;
        MaxBreakAmount = other.BreakAmount;
    }

    public void Damage(DamageDetail value){
        Health -= value.dmg;
        BreakAmount -= value.breakdmg;

        HealthPercent = Health/MaxHealth;
        BreakPercent = BreakAmount/MaxBreakAmount;
    
        if(Health <= 0f){
            Destroy(gameObject);
            return;
        }
        
    }

    public void Heal(float value){
        Health += value;
        if(Health > MaxHealth){
            Health = MaxHealth;
        }
        HealthPercent = Health/MaxHealth;
    }

    public void AddDamageSource(Damage damage){
        DamageInputs.AddLast(damage);
    }

    public void ProcessDamage(){
        LinkedListNode<Damage> head = DamageInputs.First;
        LinkedListNode<Damage> placeholder;
        while(head != null){
            placeholder = head.Next; 
            DamageDetail newdamage = head.Value.CalculateDamage(this,applied);
            Damage(newdamage);
            if(newdamage.type != DamageTypes.Cyan && newdamage.type != DamageTypes.Magenta && newdamage.type != DamageTypes.Yellow ){
                if(applied == DamageTypes.Null){
                    applied = newdamage.type;
                } else{
                    applied = DamageTypes.Null;
                }
            }
            if(head.Value.ticks < 1){
                DamageInputs.Remove(head);
            }
            onHealthChange?.Invoke(HealthPercent,BreakPercent,applied);
            head = placeholder;
        }
    }
}

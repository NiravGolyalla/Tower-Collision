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
    public float Health{get;private set;}
    public float MaxHealth{get;private set;}
    public float BreakAmount{get;private set;}
    public float MaxBreakAmount{get;private set;}
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
            // print(BreakPercent);
        }
    }

    public void LoadHealthStats(HealthStats other)
    {
        Health = other.Health;
        MaxHealth = other.Health;
        Defense = other.Defense;
        BreakAmount = other.BreakAmount;
        MaxBreakAmount = other.BreakAmount;
        HealthPercent = Health/MaxHealth;
        BreakPercent = BreakAmount/MaxBreakAmount;
    }

    public void Damage(DamageDetail value){
        Health -= value.dmg;
        BreakAmount -= value.breakdmg;

        if(Health <= 0f){
            mainSystem.Death();
            return;
        }
        if(BreakAmount <= 0f){
            BreakAmount = 0f;
        }
        
        HealthPercent = Health/MaxHealth;
        BreakPercent = BreakAmount/MaxBreakAmount;
        
    }

    public void Heal(float value){
        Health += value;
        if(Health > MaxHealth){
            Health = MaxHealth;
        }
        HealthPercent = Health/MaxHealth;
        onHealthChange?.Invoke(HealthPercent,BreakPercent,applied);
    }

    public void RegenBreak(float value){
        BreakAmount += value;
        if(BreakAmount > MaxBreakAmount){
            BreakAmount = MaxBreakAmount;
        }
        BreakPercent = BreakAmount/MaxBreakAmount;
        onHealthChange?.Invoke(HealthPercent,BreakPercent,applied);
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

            if(head.Value.type != DamageTypes.Cyan && head.Value.type != DamageTypes.Magenta && head.Value.type != DamageTypes.Yellow){
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

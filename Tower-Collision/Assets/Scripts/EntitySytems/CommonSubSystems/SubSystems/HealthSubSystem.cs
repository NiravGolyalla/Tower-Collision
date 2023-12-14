using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSubSystem : MonoBehaviour
{
    private StateMachine mainSystem;
    private float percent;
    private float Health;
    private float MaxHealth;
    private float BreakAmount;
    private float MaxBreakAmount;
    private float Defense;

    void Awake(){
        mainSystem = GetComponent<StateMachine>();
    }

    void Start(){
        LoadHealthStats(mainSystem.statLoader.HealthLoader());
    }

    public void LoadHealthStats(HealthStats other)
    {
        Health = other.Health;
        MaxHealth = other.Health;
        Defense = other.Defense;
        BreakAmount = other.BreakAmount;
        MaxBreakAmount = other.BreakAmount;
    }

    public void Damage(Damage value){
        Health -= value.amount;
        if(Health <= 0f){
            Destroy(gameObject);
            return;
        }
        percent = Health/MaxHealth;
        // print(name + " " + percent);
    }

    public void Heal(float value){
        Health += value;
        if(Health > MaxHealth){
            Health = MaxHealth;
        }
        percent = Health/MaxHealth;
    }
}

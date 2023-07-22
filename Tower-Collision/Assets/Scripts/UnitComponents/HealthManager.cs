using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : Subject
{
    private float percent;
    [SerializeField]private float health;
    [SerializeField]private float MaxHealth;

    public void SetHealth(float value){
        MaxHealth = value;
        health = value;
    }

    public void Damage(float value){
        health -= value;
        if(health <= 0f){
            Destroy(gameObject);
            return;
        }
        percent = health/MaxHealth;
    }

    public void Heal(float value){
        health += value;
        if(health > MaxHealth){
            health = MaxHealth;
        }
        percent = health/MaxHealth;
    }
}

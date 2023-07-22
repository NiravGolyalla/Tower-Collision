using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]protected UnitInformation stats;
    [SerializeField]protected HealthManager health;
    [SerializeField]protected Shooter attack;
    [SerializeField]protected BuffManger buffs;
    [SerializeField]protected ReactionManager reactions;
    protected Elements element;

    protected virtual void Awake(){
        health.SetHealth(stats.health);
        attack.SetStats(stats.range,stats.fireRate,stats.element,stats.damage);
        element = stats.element;
    }

    public virtual void Damage(Damage _damage){
        health.Damage(_damage.amount);
        reactions.Damage(_damage);
    }
    
}


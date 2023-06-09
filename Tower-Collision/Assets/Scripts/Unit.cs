using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]protected Elements element;
    [SerializeField]protected HealthBar health;
    [SerializeField]protected Shooter attack;
    [SerializeField]protected Elements appliedElement = Elements.Null;

    protected virtual void Awake(){
        health.setHealth(10f);
        attack.setStats(2f,1f,element);
    }

    public virtual void Damage(Damage val){
        health.Damage(val.amount);
        if(val.element != Elements.Normal){
            if(appliedElement == Elements.Null){
                appliedElement = val.element;
            } else{
                Reaction reaction = ElementsInteractions.CalculateReation(appliedElement,val.element);
                if(reaction != Reaction.Null){
                    React(reaction);
                    appliedElement = Elements.Null;
                } else{
                    appliedElement = val.element;
                }
            }
        }
    }

    protected abstract void React(Reaction reaction);
}


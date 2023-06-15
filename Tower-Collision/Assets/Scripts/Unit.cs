using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]protected Elements element;
    [SerializeField]protected HealthBar health;
    [SerializeField]protected Shooter attack;
    [SerializeField]protected Elements appliedElement = Elements.Null;
    protected float attackModifer = 1.0f;
    protected float healthModifer = 1.0f;




    protected virtual void Awake(){
        health.setHealth(10f);
        attack.setStats(2f,1f,element);
    }

    public virtual void Damage(Damage val){
        health.Damage(val.amount * healthModifer);
        if(val.element == Elements.Normal){return;}
        
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
    protected virtual void React(Reaction reaction){
        switch(reaction){
            //weakened
            case Reaction.Steam:
                StartCoroutine(Steam());
                break;
            //dot
            case Reaction.Burn:
                StartCoroutine(Burn());
                break;
            //root
            case Reaction.Root:
                StartCoroutine(Root());
                break;
            case Reaction.Illuminate:
                StartCoroutine(Illuminate());
                break;
            case Reaction.Eclipse:
                StartCoroutine(Eclipse());
                break;
            default:
                break;
        }
    }

    protected abstract IEnumerator Steam();
    protected abstract IEnumerator Burn();
    protected abstract IEnumerator Root();
    protected abstract IEnumerator Illuminate();
    protected abstract IEnumerator Eclipse();
}


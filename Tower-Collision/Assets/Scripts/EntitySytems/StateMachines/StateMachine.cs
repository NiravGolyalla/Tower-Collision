using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{    
    public Transform uiPlacement;
    public bool enableMachine{get; protected set;}
    public virtual StatLoader statLoader {get; protected set;}
    public AttackSubSystem attackSubSystem {protected set;get;} 
    public HealthSubSystem healthSubSystem {protected set;get;}
    protected State currentState;

    

    protected virtual void Awake(){
        enableMachine = false;
    }

    public abstract void StartSystem();
    public virtual void Death(){
        Destroy(gameObject);
    }

    protected void StateUpdate(){
        State nextState = currentState?.UpdateState(this);
        if(nextState != null){
            currentState = nextState;
        }
    }

    void Update(){
        if(enableMachine){
            StateUpdate();
        }
    }

}

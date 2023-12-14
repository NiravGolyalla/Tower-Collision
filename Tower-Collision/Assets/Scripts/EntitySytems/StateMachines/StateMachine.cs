using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{    
    public virtual StatLoader statLoader {get; protected set;}
    public AttackSubSystem attackSubSystem {protected set;get;} 
    public HealthSubSystem healthSubSystem {protected set;get;}
    protected State currentState;

    protected void StateUpdate(){
        State nextState = currentState?.UpdateState(this);
        if(nextState != null){
            currentState = nextState;
        }
        
    }

}

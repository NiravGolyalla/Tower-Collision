using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class State : ScriptableObject 
{
    public abstract State UpdateState(StateMachine system);
    public virtual State SwitchState(StateMachine system,State newState){
        ResetStateFlags(system);
        return newState;
    }
    public virtual void ResetStateFlags(StateMachine system){}
}

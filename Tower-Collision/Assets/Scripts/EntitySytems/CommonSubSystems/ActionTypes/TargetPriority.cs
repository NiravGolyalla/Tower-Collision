using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public abstract class TargetPriority : ScriptableObject
{
    public abstract StateMachine DetermineTarget(StateMachine system, List<StateMachine> targets);
}

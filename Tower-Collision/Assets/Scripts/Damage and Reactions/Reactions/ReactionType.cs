using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactionType : ScriptableObject
{
    public abstract DamageDetail TriggerReaction(Damage source,HealthSubSystem health);
}

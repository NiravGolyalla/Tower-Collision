using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultYellow", menuName = "Reaction/Create DefaultYellow")]
public class Yellow : ReactionType
{
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health)
    {
        //Trigger Yellow DMG
        //Trigger an dot DMG
        return new DamageDetail();
    }

    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultSameType", menuName = "Reaction/Create DefaultSameType")]
public class SameType : ReactionType
{
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health)
    {
        //Trigger increased Color DMG
        float dmgAmount = Damage.multiplier[source.type]*source.dmg;
        return new DamageDetail(dmgAmount,source.type,dmgAmount*Damage.breakmod);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCyan", menuName = "Reaction/Create DefaultCyan")]
public class Cyan : ReactionType
{
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health)
    {
        //Deal Cyan Damage
        //Attack Break Bar
        float dmgAmount = Damage.multiplier[source.type]*source.dmg;
        return new DamageDetail(dmgAmount,source.type,dmgAmount*Damage.cyanbreakmod);
    }


}

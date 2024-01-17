using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultYellow", menuName = "Reaction/Create DefaultYellow")]
public class Yellow : ReactionType
{
    public float ticks = 2f;
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health)
    {
        Damage dotDamage = new Damage(source.dmg*Damage.multiplier[DamageTypes.Yellow],DamageTypes.Yellow,ticks,source.sameTypeReaction,source.yellowReaction,source.cyanReaction,source.magentaReaction);
        health.AddDamageSource(dotDamage);
        return new DamageDetail(0,DamageTypes.Yellow,0f);
    }

    
}


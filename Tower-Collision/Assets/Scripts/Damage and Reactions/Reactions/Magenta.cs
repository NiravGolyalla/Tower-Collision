using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultMagenta", menuName = "Reaction/Create DefaultMagenta")]
public class Magenta : ReactionType
{
    public float aoeRange = 3f;
    public LayerMask TargetLayer;
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health){
        //Trigger Magenta DMG
        //Damage everything in an AOE
        Collider[] colliders = Physics.OverlapSphere(health.transform.position,aoeRange,TargetLayer);
        return new DamageDetail();
    }

}


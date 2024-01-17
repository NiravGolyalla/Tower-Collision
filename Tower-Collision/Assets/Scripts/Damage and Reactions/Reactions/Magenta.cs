using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultMagenta", menuName = "Reaction/Create DefaultMagenta")]
public class Magenta : ReactionType
{
    public float aoeRange = 10f;
    // public LayerMask TargetLayer;
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health){
        //Trigger Magenta DMG
        //Damage everything in an AOE
        Damage newDamage = new Damage(source.dmg*Damage.multiplier[DamageTypes.Magenta],DamageTypes.Magenta,0f,source.sameTypeReaction,source.yellowReaction,source.cyanReaction,source.magentaReaction);
        LayerMask TargetLayer = 1<<health.gameObject.layer;
        Collider[] colliders = Physics.OverlapSphere(health.transform.position,aoeRange,TargetLayer);
        
        foreach (Collider collider in colliders)
        {
            HealthSubSystem target = collider.transform.gameObject.GetComponent<HealthSubSystem>();
            // Debug.Log(target);

            if(target != null && target != health){
                target.AddDamageSource(newDamage);
            }
        }
        return new DamageDetail(source.dmg*Damage.multiplier[DamageTypes.Magenta],DamageTypes.Magenta,0f);
    }
}


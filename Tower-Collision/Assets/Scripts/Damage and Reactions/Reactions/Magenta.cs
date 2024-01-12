using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultMagenta", menuName = "Reaction/Create DefaultMagenta")]
public class Magenta : ReactionType
{
    public float aoeRange = 10f;
    public LayerMask TargetLayer;
    public override DamageDetail TriggerReaction(Damage source,HealthSubSystem health){
        //Trigger Magenta DMG
        //Damage everything in an AOE
        
        Damage newDamage = new Damage(source.dmg*Damage.multiplier[DamageTypes.Magenta],DamageTypes.Magenta,0f,source.sameTypeReaction,source.yellowReaction,source.cyanReaction,source.magentaReaction);
        Collider[] colliders = Physics.OverlapSphere(health.transform.position,aoeRange,health.gameObject.layer);
        Debug.Log(colliders.Length);
        Debug.Log(aoeRange);
        
        foreach (Collider collider in colliders)
        {
            
            HealthSubSystem target = collider.transform.gameObject.GetComponent<HealthSubSystem>();
            Debug.Log(target);

            if(target != null){
                target.AddDamageSource(newDamage);
            }
        }
        return new DamageDetail();
    }

}


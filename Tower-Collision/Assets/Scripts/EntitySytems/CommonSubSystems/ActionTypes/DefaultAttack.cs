using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttack", menuName = "Action/Attack/Create DefaultAttacks")]
public class DefaultAttack : AttackAction
{
    public override void PerformAttack(AttackSubSystem attacker, StateMachine target){
        Damage damage = new Damage(attacker.Attack,attacker.Element);
        if(attacker.projectile != null){
            Projectile projectile = Instantiate(attacker.projectile,attacker.firingPoint.position,Quaternion.identity);
            projectile.Setup(target,damage);
        } else {
            target.healthSubSystem.Damage(damage);
        }
    }   
}

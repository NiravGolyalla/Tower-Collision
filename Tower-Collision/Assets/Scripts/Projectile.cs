using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]StateMachine target;
    [SerializeField]float speed = 10;
    [SerializeField]Damage damage;
    
    void Update()
    {
        if(target == null || Vector3.Distance(transform.position,target.transform.position) < .1f){
            if(target != null){
                target.healthSubSystem.AddDamageSource(damage);
            }
            Destroy(gameObject);
            return;
        }    
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,Time.deltaTime*speed);
    }

    public void Setup(StateMachine _target,Damage _damage){
        target = _target;
        damage = _damage;
        GetComponent<Renderer>().material.color = Damage.colorMap[damage.type];
    }
}

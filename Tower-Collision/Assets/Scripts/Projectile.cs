using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]Transform end;
    [SerializeField]float speed;
    [SerializeField]Elements element;
    [SerializeField]float damage;
    
    void Update()
    {
        if(end == null || Vector3.Distance(transform.position,end.position) < .1f){
            if(end != null){
                end.gameObject.GetComponent<Unit>().Damage(new Damage(damage,element));
            }
            Destroy(gameObject);
            return;
        }    
        transform.position = Vector3.MoveTowards(transform.position,end.position,Time.deltaTime*speed);
    }

    public void Setup(Transform _end,Elements _element,float _damage){
        end = _end;
        element = _element;
        damage = _damage;
        GetComponent<Renderer>().material.color = ElementsInteractions.element_color[element];
    }
}

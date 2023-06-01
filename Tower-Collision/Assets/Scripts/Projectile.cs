using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]Transform end;
    [SerializeField]float speed;
    [SerializeField]float damage;


    void Update()
    {
        if(end == null || Vector3.Distance(transform.position,end.position) < .1f){
            if(end != null){
                end.gameObject.GetComponent<HealthBar>().Damage(damage);
            }
            Destroy(gameObject);
            return;
        }    
        transform.position = Vector3.MoveTowards(transform.position,end.position,Time.deltaTime*speed);
    }

    public void Setup(Transform _end){
        end = _end;
    }
}

public enum Elements{
    Fire,Water,Grass,Normal
}

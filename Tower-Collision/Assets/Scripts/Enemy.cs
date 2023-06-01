using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]Transform end;
    [SerializeField]float speed;

    void Update()
    {
        if(end == null || Vector3.Distance(transform.position,end.position) < .1f){
              Destroy(gameObject);
            return;
        }    
        transform.position = Vector3.MoveTowards(transform.position,end.position,Time.deltaTime*speed);
    }

    public void Setup(Transform _end){
        end = _end;
    }
}

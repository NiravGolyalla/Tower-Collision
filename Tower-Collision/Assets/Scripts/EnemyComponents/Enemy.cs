using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Unit
{
    [SerializeField]int index = 0;
    [SerializeField]float speed;

    protected override void Awake(){
        element = (Elements)UnityEngine.Random.Range(0, 6);
        base.Awake();
    }

    void Start(){
        GetComponent<Renderer>().material.color = ElementsInteractions.element_color[element];
    }

    void Update()
    {
        Movement();
    }

    void Movement(){
        if(EnemyPathManager.path.Count == index){
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position,EnemyPathManager.path[index].position,Time.deltaTime*speed);
        if(Vector3.Distance(transform.position,EnemyPathManager.path[index].position) < .1f){
            index++;
        }
    }
}

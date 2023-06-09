using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TowerController : Unit
{
    [SerializeField]TowerScriptableObject info;
    protected override void Awake(){
        health.setHealth(info.health);
        attack.setStats(info.range,info.fireRate,info.element);
    }

    void Update(){}

    void OnDestroy(){
        transform.parent.GetComponent<PlaceableTile>().place = false;
    }

    protected override void React(Reaction reaction)
    {
        print(reaction);
    }
}
    
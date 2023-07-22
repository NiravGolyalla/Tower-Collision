using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TowerController : Unit
{
    void OnDestroy(){
        transform.parent.GetComponent<PlaceableTile>().place = false;
    }
}
    
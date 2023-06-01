using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTile : Tile
{
    [SerializeField]Transform walkPoint;
    [SerializeField]int index;
    void Update(){
        if(EnemyPathManager.path.Count == index){
            EnemyPathManager.path.Add(walkPoint);
        }
    }
    
}

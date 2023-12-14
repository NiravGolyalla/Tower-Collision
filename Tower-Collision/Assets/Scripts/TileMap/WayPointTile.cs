using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTile : Tile
{
    [SerializeField]Transform walkPoint;
    public int index;
    void Update(){
        if(Spawner.path.Count == index){
            Spawner.path.Add(walkPoint);
        }
    }
    
}

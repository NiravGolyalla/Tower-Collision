using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTile : Tile
{
    [SerializeField]Transform spawnPoint;
    public TowerSystem place = null;

    public Transform getSpawnPoint(){return spawnPoint;}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTile : Tile
{
    [SerializeField]Transform spawnPoint;
    public bool place = false;

    public Transform getSpawnPoint(){return spawnPoint;}

}
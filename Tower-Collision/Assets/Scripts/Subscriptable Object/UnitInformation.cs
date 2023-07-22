using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitInformation : ScriptableObject
{
    //Tower Customizable Variables
    public float health = 10f;
    public float range = 10f;
    public float fireRate = 1f;
    public float damage = 5f;
    public Elements element = new Elements();
    public GameObject projectile;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class BuffInfo : ScriptableObject
{
    public float flatHealth = 10f;
    public float flatRange = 10f;
    public float flatFireRate = 1f;
    public float flatDamage = 5f;
    public float flatUnitStat = 5f;

    public float perHealth = 10f;
    public float perRange = 10f;
    public float perFireRate = 1f;
    public float perDamage = 5f;
    public float perUnitStat = 5f;
    
    public Elements element = new Elements();

    public Buff buffObj;

    void Awake(){
        
    }
    
}

public class Buff {
    private readonly BuffInfo info;
    public Buff(BuffInfo _info) {
        info = _info;
    }

    public dynamic GetValues(){
        return (info.flatHealth,info.flatRange,info.flatFireRate,info.flatDamage,info.flatUnitStat,info.perHealth,info.perRange,info.perFireRate,info.perDamage,info.perUnitStat,info.element);
    } 

}

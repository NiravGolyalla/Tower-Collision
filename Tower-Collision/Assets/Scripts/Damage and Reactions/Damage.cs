using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Damage {
    public float dmg;
    public DamageTypes type;
    public float ticks;
    public SameType sameTypeReaction;
    public Yellow yellowReaction;
    public Cyan cyanReaction;
    public Magenta magentaReaction;

    //constants
    public static Dictionary<DamageTypes, float> multiplier = new Dictionary<DamageTypes, float>
        {
            { DamageTypes.Red, 1.4f },
            { DamageTypes.Blue, 1.4f },
            { DamageTypes.Green, 1.4f },
            { DamageTypes.Cyan, 1.2f },
            { DamageTypes.Magenta, 1.2f },
            { DamageTypes.Yellow, 1.2f },
            { DamageTypes.Null, 1f }
        };
    public static Dictionary<DamageTypes, Color> colorMap = new Dictionary<DamageTypes, Color>
    {
        { DamageTypes.Red, Color.red },
        { DamageTypes.Blue, Color.blue },
        { DamageTypes.Green, Color.green },
        { DamageTypes.Cyan, Color.cyan },
        { DamageTypes.Magenta, Color.magenta },
        { DamageTypes.Yellow, Color.yellow },
        { DamageTypes.Null, Color.grey }
    };
    public static float breakmod = .25f;
    public static float cyanbreakmod = 2f;

    public Damage(float dmg, DamageTypes type, float ticks, SameType sameTypeReaction, Yellow yellowReaction, Cyan cyanReaction, Magenta magentaReaction)
    {
        this.dmg = dmg;
        this.type = type;
        this.ticks = ticks;
        this.sameTypeReaction = sameTypeReaction;
        this.yellowReaction = yellowReaction;
        this.cyanReaction = cyanReaction;
        this.magentaReaction = magentaReaction;
    }

    public DamageDetail CalculateDamage(HealthSubSystem health,DamageTypes applied){
        DamageTypes result = CalculateReation(type,applied);
        // Debug.Log(type);
        // Debug.Log(applied);
        // Debug.Log(result);
        if (result == DamageTypes.Null){
            return new DamageDetail(dmg,type,dmg*breakmod);
        }
        if (result == DamageTypes.Red ||result == DamageTypes.Blue ||result == DamageTypes.Green){
            return sameTypeReaction.TriggerReaction(this,health);
        }
        if(result == DamageTypes.Cyan){
            return cyanReaction.TriggerReaction(this,health);
        }
        if(result == DamageTypes.Magenta){
            return magentaReaction.TriggerReaction(this,health);
        }
        if(result == DamageTypes.Yellow){
            return yellowReaction.TriggerReaction(this,health);
        } 
        return new DamageDetail(0,type,0);
    }

    private static DamageTypes CalculateReation(DamageTypes applied1,DamageTypes applied2){
        if(applied1 == applied2){
            return applied1;
        }
        if(applied1 == DamageTypes.Red && applied2 == DamageTypes.Blue || applied1 == DamageTypes.Blue && applied2 == DamageTypes.Red){
            return DamageTypes.Magenta;
        }
        if(applied1 == DamageTypes.Red && applied2 == DamageTypes.Green || applied1 == DamageTypes.Green && applied2 == DamageTypes.Red){
            return DamageTypes.Yellow;
        }
        if(applied1 == DamageTypes.Green && applied2 == DamageTypes.Blue || applied1 == DamageTypes.Blue && applied2 == DamageTypes.Green){
            return DamageTypes.Cyan;
        }
        return DamageTypes.Null;
    }
}

public struct DamageDetail{
    public DamageTypes type;
    public float dmg;
    public float breakdmg;

    public DamageDetail(float _dmg,DamageTypes _type, float _breakdmg){
        dmg = _dmg;
        type = _type;
        breakdmg = _breakdmg;
    }
}

public enum DamageTypes{
    Red,Blue,Green,Cyan,Magenta,Yellow,Null
}





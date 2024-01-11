using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjUI : MonoBehaviour
{
    [SerializeField]Bar healthBar;
    [SerializeField]Bar breakBar;
    [SerializeField]Image applied;

    public void Bind(HealthSubSystem health){
        health.onHealthChange += UpdateStats;
    }

    void UpdateStats(float health,float breakAmount,DamageTypes dmg){
        healthBar.UpdateBar(health);
        breakBar.UpdateBar(breakAmount);
        applied.color = Damage.colorMap[dmg];
    }

}

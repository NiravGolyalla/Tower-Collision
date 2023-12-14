using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "Stats/Create TowerStats")]
public class TowerStats : CommonStats
{
    public TowerSearchState SearchState;
    public TowerAttackState AttackState;
    public float blockAmount;
}

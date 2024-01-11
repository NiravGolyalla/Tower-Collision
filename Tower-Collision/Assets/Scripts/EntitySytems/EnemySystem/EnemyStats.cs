using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Create EnemyStats")]
public class EnemyStats : CommonStats{
    public float speed;
    public EnemyPathFollow enemyPathFollowState;
    public EnemyApproachState enemyApproachState;
    public EnemyAttackState enemyAttackState;
    public bool randomElement;
}

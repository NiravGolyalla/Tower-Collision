using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    public static List<Transform> path;
    void Awake()
    {
        path = new List<Transform>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    public static List<Transform> path;
    void Start()
    {
        path = new List<Transform>();
    }
}

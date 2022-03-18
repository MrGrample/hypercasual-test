using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] int enemyCount;
    [SerializeField] bool isExit;
    [SerializeField] bool isEnemyLocationRandom { get; }

    public int EnemyCount
    {
        get { return enemyCount; }
        set { enemyCount = value; }
    }

    public bool IsExit
    {
        get { return isExit;  }
    }
}

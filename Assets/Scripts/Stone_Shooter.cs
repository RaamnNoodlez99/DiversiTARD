using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Shooter : MonoBehaviour
{

    public GameObject stoneBall;
    public Transform ballOrigin;
    public float ballSpawnInterval = 1;
    
    void Start()
    {
        InvokeRepeating("SpawnStoneBall", ballSpawnInterval, ballSpawnInterval);
    }

    void SpawnStoneBall()
    {
        if (ballOrigin != null && stoneBall != null)
        {
            GameObject drop = Instantiate(stoneBall, ballOrigin.position, Quaternion.identity);
        }
    }
}

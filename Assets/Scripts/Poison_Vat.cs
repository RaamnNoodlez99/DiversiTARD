using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Vat : MonoBehaviour
{
    public GameObject poisonDrop;
    public Transform poisonOrigin;
    public float poisonSpawnInterval = 1;

    void Start()
    {
        InvokeRepeating("SpawnPoisonDrop", 0f, poisonSpawnInterval);
    }

    void SpawnPoisonDrop()
    {
        if (poisonOrigin != null && poisonDrop != null)
        {
            Instantiate(poisonDrop, poisonOrigin.position, Quaternion.identity);
        }
    }
}

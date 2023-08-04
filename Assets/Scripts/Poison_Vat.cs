using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Vat : MonoBehaviour
{
    public GameObject poisonDrop;
    public Transform poisonOrigin;
    public float poisonSpawnInterval = 1;
    public int inialNumOfDrops = 8;
    public float gapBetweenDrops = 9f;

    private List<GameObject> spawnedPoisonDrops = new List<GameObject>();

    void Start()
    {
        SpawnInitialLine();
        InvokeRepeating("SpawnPoisonDrop", poisonSpawnInterval, poisonSpawnInterval);
    }

    void SpawnInitialLine()
    {
        if (poisonOrigin != null && poisonDrop != null)
        {
            Vector3 spawnPosition = poisonOrigin.position;
            for (int i = 0; i < inialNumOfDrops; i++)
            {
                GameObject drop = Instantiate(poisonDrop, spawnPosition, Quaternion.identity);
                spawnedPoisonDrops.Add(drop);
                spawnPosition.y -= gapBetweenDrops;
            }
        }
    }

    void SpawnPoisonDrop()
    {
        if (poisonOrigin != null && poisonDrop != null)
        {
            GameObject drop = Instantiate(poisonDrop, poisonOrigin.position, Quaternion.identity);
            spawnedPoisonDrops.Add(drop);
        }
    }

    void Update()
    {
        spawnedPoisonDrops.RemoveAll(drop => drop == null);
    }

    void OnDestroy()
    {
        foreach (GameObject drop in spawnedPoisonDrops)
        {
            Destroy(drop);
        }
    }
}

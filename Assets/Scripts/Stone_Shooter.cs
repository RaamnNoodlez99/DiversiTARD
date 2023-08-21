using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Shooter : MonoBehaviour
{

    public GameObject stoneBall;
    public Transform ballOrigin;
    public float ballSpawnInterval = 1;
    bool hittingSomething = false;
    
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hitting wall");
            hittingSomething = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("leaveing wall");
            hittingSomething = false;
        }
    }
}

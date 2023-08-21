using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Life : MonoBehaviour
{
    public int platformLife;

    public void LoseHealth(int totalLoss)
    {
        platformLife--;

        if (platformLife <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

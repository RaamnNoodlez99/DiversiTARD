using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Dropper : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowOrigin;
    public float dropCooldown = 1.0f; // The minimum time between arrow drops
    private float lastDropTime;

    private void Start()
    {
        lastDropTime = -dropCooldown; // Initialize to allow the first drop immediately
    }

    public void SpawnArrow()
    {
        // Check if enough time has passed since the last drop
        if (Time.time - lastDropTime >= dropCooldown && arrowOrigin != null && arrow != null)
        {
            GameObject drop = Instantiate(arrow, arrowOrigin.position, Quaternion.identity);

            // Update the last drop time to the current time
            lastDropTime = Time.time;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wooden_Man_Attack_Area : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Health>() != null && !collider.CompareTag("Ghost"))
        {
            Health enemyHealth = collider.GetComponent<Health>();
            enemyHealth.Damage(damage);
        }
    }
}

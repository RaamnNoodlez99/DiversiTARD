using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wooden_Man_Attack_Area : MonoBehaviour
{
    public int damage = 10;

    private Collider2D colliderToRemove;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Health>() != null && !collider.CompareTag("Ghost"))
        {
            colliderToRemove = collider;
            Invoke("RemoveEnemyHealth", 0.3f);
        }
        if(collider.CompareTag("switch"))
        {
            //Debug.Log("hit");
        }
    }

    private void RemoveEnemyHealth()
    {
        Health enemyHealth = colliderToRemove.GetComponent<Health>();
        enemyHealth.RemoveLife();
    }
}

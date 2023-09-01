using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Platform : MonoBehaviour
{

    public bool killsGhost = true;
    public bool killsFather = true;
    public HealthBar healthBar;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health objectHealth = collision.collider.GetComponent<Health>();
        if (objectHealth != null)
        {
            if (collision.gameObject.CompareTag("Ghost") && killsGhost)
            {
                Debug.Log("ghost entered");
                collision.gameObject.GetComponent<Health>().Die();
            }
            if (collision.gameObject.CompareTag("WoodenMan") && killsFather)
            {
                healthBar.setHealth(0);
                collision.gameObject.GetComponent<Health>().Die();
            }
        }
    }
}
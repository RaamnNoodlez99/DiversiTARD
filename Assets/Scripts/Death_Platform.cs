using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Platform : MonoBehaviour
{

    public bool killsGhost = false;
    public bool killsFather = true;
    public HealthBar healthBar;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health objectHealth = collision.collider.GetComponent<Health>();
        if (objectHealth != null)
        {
            healthBar.setHealth(0);
            if (collision.gameObject.CompareTag("Ghost") && killsGhost)
            {
                collision.gameObject.GetComponent<Health>().Die();
            }
            if (collision.gameObject.CompareTag("WoodenMan") && killsFather)
            {
                collision.gameObject.GetComponent<Health>().Die();
            }
        }
    }
}
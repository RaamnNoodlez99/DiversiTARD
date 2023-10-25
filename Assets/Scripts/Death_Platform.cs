using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Platform : MonoBehaviour
{

    public bool killsGhost = true;
    public bool killsFather = true;
    public HealthBar healthBar;
    public bool isBoss = false;

    private bool isAlreadyPlayingDeathAnimation = false;

    private Animator bossAnimator;
    private void Start()
    {
        if (isBoss)
        {
           bossAnimator = GetComponent<Animator>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health objectHealth = collision.collider.GetComponent<Health>();
        if (objectHealth != null)
        {
            if (collision.gameObject.CompareTag("Ghost") && killsGhost)
            {
                Debug.Log("ghost entered");
                collision.gameObject.GetComponent<Health>().Die();

                if (isBoss)
                {
                    if (!isAlreadyPlayingDeathAnimation)
                    {
                        bossAnimator.SetBool("ghostDeath", true);
                        isAlreadyPlayingDeathAnimation = true;
                    }
                }
            }
            if (collision.gameObject.CompareTag("WoodenMan") && killsFather)
            {
                healthBar.setHealth(0);
                collision.gameObject.GetComponent<Health>().Die();
                
                if (isBoss)
                {
                    if (!isAlreadyPlayingDeathAnimation)
                    {
                        bossAnimator.SetBool("dadDeath", true);
                        isAlreadyPlayingDeathAnimation = true;
                    }
                }
            }
        }
        
    }
}
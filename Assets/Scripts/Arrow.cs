using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float destroyDelay = 0.2f;
    public int damage = 5;


    private void Awake()
    {
    }

    public void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("Hit Boss");
        }
            Health objectHealth = collision.collider.GetComponent<Health>();

        if (objectHealth != null)
        {
            Player_Controller player = null;
            if (collision.gameObject.CompareTag("Ghost"))
            {
                player = GameObject.FindGameObjectWithTag("Ghost").GetComponent<Player_Controller>();
            }
            else if (collision.gameObject.CompareTag("WoodenMan"))
            {
                player = GameObject.FindGameObjectWithTag("WoodenMan").GetComponent<Player_Controller>();
            }
            else if (collision.gameObject.CompareTag("Boss"))
            {
                collision.gameObject.GetComponent<Boss_Phase3>().flickerBoss();
            }
            
            if (player != null)
            {
                player.knockbackCounter = player.knockbackTotalTime;

                if (collision.transform.position.x <= transform.position.x)
                {
                    player.knockFromRight = true;
                }
                else
                {
                    player.knockFromRight = false;
                }

                objectHealth.Damage(damage);
            }
            else
            {
                objectHealth.Damage(damage);
            }
        }

        if (collision.collider != null && !collision.gameObject.CompareTag("Projectile"))
        {
            //SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.poisonDrop, 0.3f);
            Destroy(gameObject, destroyDelay);
        }
    }
}

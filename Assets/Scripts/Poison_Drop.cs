using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Drop : MonoBehaviour
{
    public float destroyDelay = 0.2f;
    public int damage = 5;
    public Player_Controller player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health objectHealth = collision.collider.GetComponent<Health>();
        if (objectHealth != null)
        {
            // Determine which player has been hit based on tags
            Player_Controller player = null;
            if (collision.gameObject.CompareTag("Ghost"))
            {
                Debug.Log("Ghost hit");
                player = GameObject.FindGameObjectWithTag("Ghost").GetComponent<Player_Controller>();
            }
            else if (collision.gameObject.CompareTag("WoodenMan"))
            {
                Debug.Log("Father hit");
                player = GameObject.FindGameObjectWithTag("WoodenMan").GetComponent<Player_Controller>();
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
        }

        if (collision.collider != null && !collision.gameObject.CompareTag("Projectile"))
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.poisonDrop);
            Destroy(gameObject, destroyDelay);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Drop : MonoBehaviour
{
    public float destroyDelay = 0.2f;
    public int damage = 5;
    public Player_Controller player;
    public Animator dropAnimator;
    public Vector2 positionOffset = new Vector2(-0.3f, 0.5f);


    private void Awake()
    {
        dropAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
            if (!collision.gameObject.CompareTag("WoodenMan"))
            {
                Vector2 newPosition = (Vector2)gameObject.transform.position + positionOffset;
                gameObject.transform.position = newPosition;
                dropAnimator.SetTrigger("Hit");
            }
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.poisonDrop,0.3f);
            Destroy(gameObject, destroyDelay);
        }
    }
}

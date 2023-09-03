using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stone_Ball : MonoBehaviour
{
    public float timeToDestroy;
    public AudioSource stoneSource;
    public AudioClip[] stoneFallSounds;
    public int damage = 50;

    // Keep track of the last played audio clip.
    private AudioClip lastPlayedSound;
    private GameObject woodenManReference;
    private Character_Switch woodenMansCharacterSwitch;
    private SpriteRenderer spriteRenderer;

    public Sprite woodenManRock;
    public Sprite ghostRock;

    public bool doDamage = false;
    
    private void Start()
    {
        woodenManReference = GameObject.Find("Wooden Man");
        woodenMansCharacterSwitch = woodenManReference.GetComponent<Character_Switch>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (woodenMansCharacterSwitch.getCurCharacter() == "WoodenMan")
            spriteRenderer.sprite = woodenManRock;
        else
            spriteRenderer.sprite = ghostRock;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       /* Health objectHealth = collision.collider.GetComponent<Health>();

        if (objectHealth != null)
        {
            Player_Controller player = null;
            if (collision.gameObject.CompareTag("WoodenMan"))
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
        }*/


        if (collision.collider != null && !collision.gameObject.CompareTag("StoneBall"))
        {
           gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            AudioClip randomSound;
        
            // Keep generating a random sound until it's different from the last one played.
            do
            {
                randomSound = stoneFallSounds[Random.Range(0, stoneFallSounds.Length)];
            } while (randomSound == lastPlayedSound);
        
            stoneSource.clip = randomSound;
            stoneSource.Play();
        
            // Update the last played sound.
            lastPlayedSound = randomSound;
        
            Destroy(gameObject, randomSound.length);
        }
        
        if (doDamage && (collision.gameObject.CompareTag("WoodenMan") || collision.gameObject.CompareTag("Ghost")))
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
                    player.knockbackForce = 25;
                    player.knockbackCounter = player.knockbackTotalTime;

                    if (collision.transform.position.x <= transform.position.x)
                    {
                        player.knockFromRight = true;
                    }
                    else
                    {
                        player.knockFromRight = false;
                    }

                    objectHealth.Damage(10);
                }
            }
        }
    }

    public float playRandomAudio()
    {
        AudioClip randomSound;
        do
        {
            randomSound = stoneFallSounds[Random.Range(0, stoneFallSounds.Length)];
        } while (randomSound == lastPlayedSound);

        stoneSource.clip = randomSound;
        stoneSource.Play();
        lastPlayedSound = randomSound;

        return randomSound.length;
    }
}

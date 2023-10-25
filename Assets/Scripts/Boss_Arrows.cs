using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss_Arrows : MonoBehaviour
{
    public float timeToDestroy;
    public AudioSource stoneSource;
    public AudioClip[] stoneFallSounds;
    public int damage = 50;
    public LayerMask platformLayer;
    public bool ignoreFirstPlatform = false;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, platformLayer);

        if (hit.collider != null && hit.collider.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }

        woodenManReference = GameObject.Find("Wooden Man");
        if (woodenManReference != null)
            woodenMansCharacterSwitch = woodenManReference.GetComponent<Character_Switch>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (woodenMansCharacterSwitch)
        {
            if (woodenMansCharacterSwitch.getCurCharacter() == "WoodenMan")
                spriteRenderer.sprite = woodenManRock;
            else
                spriteRenderer.sprite = ghostRock;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow Pass"))
        {
            if (ignoreFirstPlatform)
            {
                Debug.Log("2");
                gameObject.layer = LayerMask.NameToLayer("Ignore Platforms");
                Invoke("ChangeLayer", 0.32f);
                ignoreFirstPlatform = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider != null && !collision.gameObject.CompareTag("StoneBall"))
        {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                AudioClip randomSound;

                do
                {
                    randomSound = stoneFallSounds[Random.Range(0, stoneFallSounds.Length)];
                } while (randomSound == lastPlayedSound);

                stoneSource.clip = randomSound;
                stoneSource.Play();
                lastPlayedSound = randomSound;

                Destroy(gameObject, randomSound.length);   
        }

        if (doDamage && (collision.gameObject.CompareTag("WoodenMan")))
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
                    player.knockbackForce = 35;
                    player.knockbackCounter = player.knockbackTotalTime;

                    if (collision.transform.position.x <= transform.position.x)
                    {
                        player.knockFromRight = true;
                    }
                    else
                    {
                        player.knockFromRight = false;
                    }

                    objectHealth.RemoveLife();
                }
            }
        }
    }

    private void ChangeLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Arrows");
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

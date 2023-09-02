using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    public Sprite woodenManPlatformSprite;
    public Sprite ghostPlatformSprite;
    public float destroyTime;
    public float restoreTime;
    public bool shouldRestore = true;
    public AudioSource crackSound;
    public AudioSource rattleSound;


    bool hasCracked = false;

    private Rigidbody2D rb;
    private Animator childAnimator;
    private SpriteRenderer childRenderer;
    private GameObject characterCheck;
    private Vector3 initialPosition;
    

    void Start()
    {
        characterCheck = GameObject.Find("Ghost");
        rb = GetComponent<Rigidbody2D>();
        Transform platformSprite = transform.GetChild(0);
        childAnimator = platformSprite.GetComponent<Animator>();
        childRenderer = platformSprite.GetComponent<SpriteRenderer>();
        initialPosition = transform.position; 
    }

    private void Update()
    {
        if (characterCheck != null)
        {
            if (characterCheck.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
                childRenderer.sprite = woodenManPlatformSprite;
            else
                childRenderer.sprite = ghostPlatformSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodenMan") || other.gameObject.CompareTag("Ghost"))
        {
            if (!hasCracked)
            {
                crackSound.Play();
                hasCracked = true;
            }

            Invoke("AnimatePlatform", 0.5f);
            //Destroy(gameObject, destroyTime + 2);
        }
    }

    void AnimatePlatform()
    {
        rattleSound.loop = true;
        rattleSound.Play();
        childAnimator.enabled = true;
        Invoke("DropPlatform", destroyTime);
    }

    void DropPlatform()
    {
        rattleSound.Stop();
        rb.isKinematic = false;
        hasCracked = false;

        if(shouldRestore)
            Invoke("RestorePlatform", restoreTime);
    }

    void RestorePlatform()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        transform.position = initialPosition; 
        childAnimator.enabled = false;
    }
}

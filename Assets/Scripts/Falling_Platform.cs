using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    public Sprite woodenManPlatformSprite;
    public Sprite ghostPlatformSprite;
    
    private Rigidbody2D rb;
    private Animator childAnimator;
    private SpriteRenderer childRenderer;
    private GameObject characterCheck;
    void Start()
    {
        characterCheck = GameObject.Find("Ghost");
        rb = GetComponent<Rigidbody2D>();
        Transform platformSprite = transform.GetChild(0);
        childAnimator = platformSprite.GetComponent<Animator>();
        childRenderer = platformSprite.GetComponent<SpriteRenderer>();
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
            Invoke("AnimatePlatform", 0.5f);
            Destroy(gameObject, 4.5f);
        }
    }
    
    void AnimatePlatform()
    {
        childAnimator.enabled = true;
        
        Invoke("DropPlatform", 2.5f);
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}

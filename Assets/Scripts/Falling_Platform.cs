using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator childAnimator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Transform platformSprite = transform.GetChild(0);
        childAnimator = platformSprite.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodenMan"))
        {
            Invoke("AnimatePlatform", 0.5f);
            Destroy(gameObject, 4.5f);
        }
    }
    
    void AnimatePlatform()
    {
        childAnimator.enabled = true;
        
        Invoke("DropPlatform", 3f);
    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform : MonoBehaviour
{
    public float destroyTime;
    public float restoreTime;
    public bool shouldRestore = true;
    public AudioSource crackSound;
    public AudioSource rattleSound;
    public Sprite startingPlatformSprite;


    bool hasCracked = false;

    private Rigidbody2D rb;
    private Animator childAnimator;
    private SpriteRenderer childRenderer;
    private GameObject characterCheck;
    private Vector3 initialPosition;

    public Color dadPlatformColor;
    public Color ghostPlatformColor;

    public bool playFast = false;

    void Start()
    {
        characterCheck = GameObject.Find("Ghost");
        rb = GetComponent<Rigidbody2D>();
        Transform platformSprite = transform.GetChild(0);
        childAnimator = platformSprite.GetComponent<Animator>();
        childRenderer = platformSprite.GetComponent<SpriteRenderer>();
        childRenderer.color = Color.white;
        initialPosition = transform.position; 
    }

    private void Update()
    {
        if (characterCheck != null)
        {
            if (characterCheck.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                childRenderer.color = dadPlatformColor;
            }
            else
            {
                childRenderer.color = ghostPlatformColor;
            }
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

            if (playFast)
                AnimatePlatform();
            else
                Invoke("AnimatePlatform", 0f);
            //Destroy(gameObject, destroyTime + 2);
        }
    }

    void AnimatePlatform()
    {
        rattleSound.loop = true;
        rattleSound.Play();
        childAnimator.SetBool("shakePlatform", true);
        childAnimator.SetBool("dropPlatform", false);
        childAnimator.SetBool("makeIdle", false);
        
        if (playFast)
            SetDrop();
        else
            Invoke("SetDrop", destroyTime - 2);
    }

    void SetDrop()
    {
        childAnimator.SetBool("shakePlatform", false);
        childAnimator.SetBool("dropPlatform", true);
        childAnimator.SetBool("makeIdle", false);
        
        if (playFast)
            Invoke("DropPlatform", 1);
        else
            Invoke("DropPlatform", 1);
    }

    void DropPlatform()
    {
        int layerIndex = LayerMask.NameToLayer("IgnorePlayer");
        SetLayerRecursively(gameObject, layerIndex);
        
        rattleSound.Stop();
        rb.isKinematic = false;
        hasCracked = false;

        if(shouldRestore)
            Invoke("SetRestore", 1);
    }
    
    void SetRestore()
    {
        childAnimator.SetBool("shakePlatform", false);
        childAnimator.SetBool("dropPlatform", false);
        childAnimator.SetBool("makeIdle", true);
        Invoke("RestorePlatform", restoreTime - 1);
    }

    void RestorePlatform()
    {
        int layerIndex = LayerMask.NameToLayer("Default");
        SetLayerRecursively(gameObject, layerIndex);
        
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        transform.position = initialPosition;
        childRenderer.sprite = startingPlatformSprite;
    }
    
    void SetLayerRecursively(GameObject obj, int layerIndex)
    {
        obj.layer = layerIndex;
        
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerIndex);
        }
    }
}


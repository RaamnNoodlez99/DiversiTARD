using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Head_Swapper : MonoBehaviour
{
    public Sprite circleSpriteDad;
    public Sprite headSpriteDad;
    public Sprite mouthSpriteDad;
    public Sprite mouthOpenSpriteDad;
    public Sprite eyesSpriteDad;
    
    public Sprite circleSpriteGhost;
    public Sprite headSpriteGhost;
    public Sprite mouthSpriteGhost;
    public Sprite mouthOpenSpriteGhost;
    public GameObject followShooter;
    public Sprite eyesSpriteGhost;

    private bool followOnlyGhost;
    private bool followOnlyDad;

    public GameObject circle;
    public GameObject head;
    public GameObject mouth;
    public GameObject mouthOpen;
    public GameObject eyes;


    private void Start()
    {
        followOnlyGhost = followShooter.GetComponent<Follow_Shooter>().followOnlyGhost;
        followOnlyDad = followShooter.GetComponent<Follow_Shooter>().followOnlyWoodenMan;

        if (followOnlyGhost)
        {
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.sprite = circleSpriteGhost;

            SpriteRenderer faceRenderer = head.GetComponent<SpriteRenderer>();
            faceRenderer.sprite = headSpriteGhost;

            SpriteRenderer mouthRenderer = mouth.GetComponent<SpriteRenderer>();
            mouthRenderer.sprite = mouthSpriteGhost;

            SpriteRenderer mouthOpenRenderer = mouthOpen.GetComponent<SpriteRenderer>();
            mouthOpenRenderer.sprite = mouthOpenSpriteGhost;
            
            SpriteRenderer eyesRenderer = eyes.GetComponent<SpriteRenderer>();
            eyesRenderer.sprite = eyesSpriteGhost;
        }
    }

    public void swapToDadShooterHead()
    {
        if (!followOnlyGhost)
        {
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.sprite = circleSpriteDad;

            SpriteRenderer faceRenderer = head.GetComponent<SpriteRenderer>();
            faceRenderer.sprite = headSpriteDad;

            SpriteRenderer mouthRenderer = mouth.GetComponent<SpriteRenderer>();
            mouthRenderer.sprite = mouthSpriteDad;

            SpriteRenderer mouthOpenRenderer = mouthOpen.GetComponent<SpriteRenderer>();
            mouthOpenRenderer.sprite = mouthOpenSpriteDad;
            
            SpriteRenderer eyesRenderer = eyes.GetComponent<SpriteRenderer>();
            eyesRenderer.sprite = eyesSpriteDad;
        }
       
    }
    
    public void swapToGhostShooterHead()
    {
        if (!followOnlyDad)
        {
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.sprite = circleSpriteGhost;

            SpriteRenderer faceRenderer = head.GetComponent<SpriteRenderer>();
            faceRenderer.sprite = headSpriteGhost;

            SpriteRenderer mouthRenderer = mouth.GetComponent<SpriteRenderer>();
            mouthRenderer.sprite = mouthSpriteGhost;

            SpriteRenderer mouthOpenRenderer = mouthOpen.GetComponent<SpriteRenderer>();
            mouthOpenRenderer.sprite = mouthOpenSpriteGhost;
            
            SpriteRenderer eyesRenderer = eyes.GetComponent<SpriteRenderer>();
            eyesRenderer.sprite = eyesSpriteGhost;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Head_Swapper : MonoBehaviour
{
    public Sprite circleSpriteDad;
    public Sprite headSpriteDad;
    public Sprite mouthSpriteDad;
    public Sprite mouthOpenSpriteDad;
    
    public Sprite circleSpriteGhost;
    public Sprite headSpriteGhost;
    public Sprite mouthSpriteGhost;
    public Sprite mouthOpenSpriteGhost;
    public GameObject followShooter;

    private bool followOnlyGhost;
    private bool followOnlyDad;

    public GameObject circle;
    public GameObject head;
    public GameObject mouth;
    public GameObject mouthOpen;


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
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail_Swapper : MonoBehaviour
{
    public Sprite centerRailSpriteDad;
    public Sprite leftRailSpriteDad;
    public Sprite rightRailSpriteDad;
    
    public Sprite centerRailSpriteGhost;
    public Sprite leftRailSpriteGhost;
    public Sprite rightRailSpriteGhost;

    private bool followOnlyGhost;
    private bool followOnlyDad;

    public GameObject centerRail;
    public GameObject leftRail;
    public GameObject rightRail;
    public GameObject followShooter;


    private void Start()
    {
        followOnlyGhost = followShooter.GetComponent<Follow_Shooter>().followOnlyGhost;
        followOnlyDad = followShooter.GetComponent<Follow_Shooter>().followOnlyWoodenMan;

        if (followOnlyGhost)
        {
            SpriteRenderer centerRenderer = centerRail.GetComponent<SpriteRenderer>();
            centerRenderer.sprite = centerRailSpriteGhost;

            SpriteRenderer leftRenderer = leftRail.GetComponent<SpriteRenderer>();
            leftRenderer.sprite = leftRailSpriteGhost;

            SpriteRenderer rightRenderer = rightRail.GetComponent<SpriteRenderer>();
            rightRenderer.sprite = rightRailSpriteGhost;
        }
    }

    public void swapToDadRails()
    {
        if (!followOnlyGhost)
        {
            SpriteRenderer centerRenderer = centerRail.GetComponent<SpriteRenderer>();
            centerRenderer.sprite = centerRailSpriteDad;

            SpriteRenderer leftRenderer = leftRail.GetComponent<SpriteRenderer>();
            leftRenderer.sprite = leftRailSpriteDad;

            SpriteRenderer rightRenderer = rightRail.GetComponent<SpriteRenderer>();
            rightRenderer.sprite = rightRailSpriteDad;
        }
        
    }
    
    public void swapToGhostRails()
    {
        if (!followOnlyDad)
        {
            SpriteRenderer centerRenderer = centerRail.GetComponent<SpriteRenderer>();
            centerRenderer.sprite = centerRailSpriteGhost;

            SpriteRenderer leftRenderer = leftRail.GetComponent<SpriteRenderer>();
            leftRenderer.sprite = leftRailSpriteGhost;

            SpriteRenderer rightRenderer = rightRail.GetComponent<SpriteRenderer>();
            rightRenderer.sprite = rightRailSpriteGhost;
        }
        
    }
}

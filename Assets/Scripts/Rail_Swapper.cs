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

    public GameObject centerRail;
    public GameObject leftRail;
    public GameObject rightRail;
    
    public void swapToDadRails()
    {
        SpriteRenderer centerRenderer = centerRail.GetComponent<SpriteRenderer>();
        centerRenderer.sprite = centerRailSpriteDad;

        SpriteRenderer leftRenderer = leftRail.GetComponent<SpriteRenderer>();
        leftRenderer.sprite = leftRailSpriteDad;

        SpriteRenderer rightRenderer = rightRail.GetComponent<SpriteRenderer>();
        rightRenderer.sprite = rightRailSpriteDad;
    }
    
    public void swapToGhostRails()
    {
        SpriteRenderer centerRenderer = centerRail.GetComponent<SpriteRenderer>();
        centerRenderer.sprite = centerRailSpriteGhost;

        SpriteRenderer leftRenderer = leftRail.GetComponent<SpriteRenderer>();
        leftRenderer.sprite = leftRailSpriteGhost;

        SpriteRenderer rightRenderer = rightRail.GetComponent<SpriteRenderer>();
        rightRenderer.sprite = rightRailSpriteGhost;
    }
}

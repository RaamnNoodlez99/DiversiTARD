using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp_Swapper : MonoBehaviour
{
    public SpriteRenderer seedLeafSpriteRenderer;
    public SpriteRenderer seedSpriteRenderer;
    public SpriteRenderer vineBackRenderer;
    public SpriteRenderer vineFrontRenderer;
    
    public Color dadSeedLeafColor;
    public Color dadSeedColor;
    public Color dadVineBackColor;
    public Color dadVineFrontColor;
    
    public Color ghostSeedLeafColor;
    public Color ghostSeedColor;
    public Color ghostVineBackColor;
    public Color ghostVineFrontColor;
    
    private GameObject characterCheck;

    private void Start()
    {
        characterCheck = GameObject.Find("Ghost");
    }

    private void Update()
    {
        if (characterCheck != null)
        {
            if (characterCheck.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                seedLeafSpriteRenderer.color = dadSeedLeafColor;
                seedSpriteRenderer.color = dadSeedColor;
                vineBackRenderer.color = dadVineBackColor;
                vineFrontRenderer.color = dadVineFrontColor;
            }
            else
            {
                seedLeafSpriteRenderer.color = ghostSeedLeafColor;
                seedSpriteRenderer.color = ghostSeedColor;
                vineBackRenderer.color = ghostVineBackColor;
                vineFrontRenderer.color = ghostVineFrontColor;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GroundCheck : MonoBehaviour
{
    public Player_Controller characterToCheckController;

    public GameObject ghostReference;

    public GameObject RespawnPlatformHUDIcon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Bone"))
        {
            characterToCheckController.setIsJumping(false);

            // if (gameObject.name == "Ground Check Ghost")
            // {
            //     Player_Controller ghostController = ghostReference.GetComponent<Player_Controller>();
            //
            //     if (ghostController.ghostPlatformExists && collision.gameObject.CompareTag("Platform"))
            //     {
            //         Image platformHUDImage = RespawnPlatformHUDIcon.GetComponent<Image>();
            //         Color currentColor = platformHUDImage.color;
            //         currentColor.a = 1f;
            //         platformHUDImage.color = currentColor;
            //         ghostController.ghostPlatformExists = false;
            //     } else if (ghostController.ghostPlatformExists && collision.gameObject.CompareTag("Ghost Bone"))
            //     {
            //         Image platformHUDImage = RespawnPlatformHUDIcon.GetComponent<Image>();
            //         Color currentColor = platformHUDImage.color;
            //         currentColor.a = 0.5f;
            //         platformHUDImage.color = currentColor;
            //         ghostController.ghostPlatformExists = true;
            //     }
            // }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
        {
            characterToCheckController.setIsJumping(true);
        }
    }
}

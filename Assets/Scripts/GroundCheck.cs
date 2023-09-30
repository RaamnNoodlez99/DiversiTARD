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

    private GameObject dadObject;

    private void Awake()
    {
        dadObject = GameObject.FindWithTag("WoodenMan");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Bone") || collision.gameObject.CompareTag("Button"))
        { 
            GameObject parentObject = this.transform.parent.gameObject;
            string currChar = parentObject.GetComponent<Character_Switch>().getCurCharacter();

            if (currChar == "WoodenMan" && collision.gameObject.CompareTag("Button"))
            {
                characterToCheckController.setIsJumping(false);
            } else if (currChar == "Ghost" && collision.gameObject.CompareTag("Button"))
            {
                return;
            } else if (currChar == "Ghost" && collision.gameObject.CompareTag("Platform"))
            {
                if (ghostReference.GetComponent<FollowCharacter>() && collision.gameObject.name.Contains("Bone"))
                {
                    dadObject.GetComponent<Player_Controller>().ghostCanFollow = false;
                    ghostReference.GetComponent<FollowCharacter>().enabled = false;
                }
            }
                
            characterToCheckController.setIsJumping(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform") || collision.gameObject.CompareTag("Button"))
        {
            GameObject parentObject = this.transform.parent.gameObject;
            string currChar = parentObject.GetComponent<Character_Switch>().getCurCharacter();

            if (currChar == "WoodenMan" && collision.gameObject.CompareTag("Button"))
            {
                characterToCheckController.setIsJumping(true);
            } else if (currChar == "Ghost" && collision.gameObject.CompareTag("Button"))
            {
                return;
            } else if (currChar == "Ghost" && collision.gameObject.CompareTag("Platform"))
            {
                if (ghostReference.GetComponent<FollowCharacter>() && collision.gameObject.name.Contains("Bone"))
                {
                    dadObject.GetComponent<Player_Controller>().ghostCanFollow = true;
                }
            }
            
            characterToCheckController.setIsJumping(true);
        }
    }
}

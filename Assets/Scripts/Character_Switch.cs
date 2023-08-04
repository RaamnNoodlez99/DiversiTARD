using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Character_Switch : MonoBehaviour
{
    public GameObject otherCharacter;
    public GameObject thisCharacter;

    public CinemachineVirtualCamera cinemachine;
    private Transform activeCharacter;
    public static string currentCharacter;

    public string getCurCharacter()
    {
        return currentCharacter;
    }

    private void Awake()
    {
        if (thisCharacter.CompareTag("WoodenMan"))
        {
            currentCharacter = "WoodenMan";
            Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();

            thisCharacter.GetComponent<Player_Controller>().enabled = true;
            otherCharacter.GetComponent<Player_Controller>().enabled = false;
            //otherCharacter.GetComponent<Character_Switch>().enabled = false;
            activeCharacter = thisCharacter.transform;
            cinemachine.LookAt = activeCharacter;
            cinemachine.Follow = activeCharacter;
            thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
        }
        else
        {
            activeCharacter = otherCharacter.transform;
        }

        Physics2D.IgnoreCollision(thisCharacter.GetComponent<CapsuleCollider2D>(), otherCharacter.GetComponent<CapsuleCollider2D>());
    }

    public void SwitchCharacter()
    {
        //Debug.Log("Inside function");
        //Debug.Log("before: " + activeCharacter.tag);
       // Debug.Log("Active: " + activeCharacter.tag);
        if ( activeCharacter != null )
        {
            if (activeCharacter.CompareTag("WoodenMan"))
            {
                if (thisCharacter.CompareTag("Ghost"))
                {
                    //Debug.Log("One");
                    Environment_Handler.evironmentHandlerInstance.spawnGhostEnvironment();
                    currentCharacter = "Ghost";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    thisCharacter.GetComponent<Player_Controller>().enabled = true;
                    otherCharacter.GetComponent<Player_Controller>().enabled = false;
                    activeCharacter = thisCharacter.transform;
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, 0f);
                    //otherCharacter.GetComponent<Character_Switch>().enabled = false;
                }
                else
                {
                    //Debug.Log("Two");
                    Environment_Handler.evironmentHandlerInstance.spawnGhostEnvironment();
                    currentCharacter = "Ghost";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    otherCharacter.GetComponent<Player_Controller>().enabled = true;
                    thisCharacter.GetComponent<Player_Controller>().enabled = false;
                    activeCharacter = otherCharacter.transform;
                    //Debug.Log(activeCharacter.tag);
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, -0.1f);
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, 0f);
                    //thisCharacter.GetComponent<Character_Switch>().enabled = false;
                }
            }
            else if (activeCharacter.CompareTag("Ghost"))
            {
                if (thisCharacter.CompareTag("WoodenMan"))
                {
                    //Debug.Log("Three");
                    Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();
                    currentCharacter = "WoodenMan";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    thisCharacter.GetComponent<Player_Controller>().enabled = true;
                    otherCharacter.GetComponent<Player_Controller>().enabled = false;
                    activeCharacter = thisCharacter.transform;
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, 0f);
                    //otherCharacter.GetComponent<Character_Switch>().enabled = false;
                }
                else
                {
                    //Debug.Log("Four");
                    Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();
                    currentCharacter = "WoodenMan";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    otherCharacter.GetComponent<Player_Controller>().enabled = true;
                    thisCharacter.GetComponent<Player_Controller>().enabled = false;
                    activeCharacter = otherCharacter.transform;
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, -0.1f);
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, 0f);
                    //thisCharacter.GetComponent<Character_Switch>().enabled = false;
                }
            }

            cinemachine.LookAt = activeCharacter;
            cinemachine.Follow = activeCharacter;
        }
    }

}
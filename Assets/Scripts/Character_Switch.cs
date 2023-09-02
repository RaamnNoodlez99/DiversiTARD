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
    
    public GameObject healthBar;

    public string getCurCharacter()
    {
        return currentCharacter;
    }

    private void Awake()
    {
        if (thisCharacter.CompareTag("WoodenMan"))
        {
            currentCharacter = "WoodenMan";
            if(Environment_Handler.evironmentHandlerInstance != null)
                Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();

            thisCharacter.GetComponent<Player_Controller>().enabled = true;
            otherCharacter.GetComponent<Player_Controller>().enabled = false;
            //otherCharacter.GetComponent<Character_Switch>().enabled = false;
            activeCharacter = thisCharacter.transform;
            
            foreach (Renderer renderer in activeCharacter.GetComponentsInChildren<Renderer>())
            {
                renderer.sortingOrder += 10;
            }
                    
            foreach (Renderer renderer in otherCharacter.GetComponentsInChildren<Renderer>())
            {
                renderer.sortingOrder -= 10;
            }

            if (cinemachine)
            {
                cinemachine.LookAt = activeCharacter;
                cinemachine.Follow = activeCharacter;
            }

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

            otherCharacter.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            thisCharacter.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (activeCharacter.CompareTag("WoodenMan"))
            {
                healthBar.SetActive(false);

                if (thisCharacter.CompareTag("Ghost"))
                {
                    //Debug.Log("One");
                    if (Environment_Handler.evironmentHandlerInstance != null)
                        Environment_Handler.evironmentHandlerInstance.spawnGhostEnvironment();

                    currentCharacter = "Ghost";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    thisCharacter.GetComponent<Player_Controller>().enabled = true;
                    otherCharacter.GetComponent<Player_Controller>().enabled = false;

                    Rigidbody2D otherCharactersRigidBody = otherCharacter.GetComponent<Rigidbody2D>();
                    otherCharactersRigidBody.gravityScale = 25f;

                    Animator otherCharactersAnimator = otherCharacter.GetComponent<Animator>();
                    otherCharactersAnimator.SetFloat("Speed", 0);
                    
                    activeCharacter = thisCharacter.transform;
                    
                    foreach (Renderer renderer in activeCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder += 10;
                    }
                    
                    foreach (Renderer renderer in otherCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder -= 10;
                    }
                    
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, 0f);
                    //otherCharacter.GetComponent<Character_Switch>().enabled = false;
                }
                else
                {
                    if (Environment_Handler.evironmentHandlerInstance != null)
                        Environment_Handler.evironmentHandlerInstance.spawnGhostEnvironment();

                    currentCharacter = "Ghost";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    otherCharacter.GetComponent<Player_Controller>().enabled = true;
                    thisCharacter.GetComponent<Player_Controller>().enabled = false;
                    
                    Rigidbody2D thisCharactersRigidBody = thisCharacter.GetComponent<Rigidbody2D>();
                    thisCharactersRigidBody.gravityScale = 25f;
                    
                    Animator thisCharactersAnimator = thisCharacter.GetComponent<Animator>();
                    thisCharactersAnimator.SetFloat("Speed", 0);
                    
                    activeCharacter = otherCharacter.transform;
                    
                    foreach (Renderer renderer in activeCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder += 10;
                    }
                    
                    foreach (Renderer renderer in thisCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder -= 10;
                    }
                    //Debug.Log(activeCharacter.tag);
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, -0.1f);
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, 0f);
                    //thisCharacter.GetComponent<Character_Switch>().enabled = false;
                }
            }
            else if (activeCharacter.CompareTag("Ghost"))
            {
                healthBar.SetActive(true);
                
                if (thisCharacter.CompareTag("WoodenMan"))
                {
                    //Debug.Log("Three");
                    if (Environment_Handler.evironmentHandlerInstance != null)
                        Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();

                    currentCharacter = "WoodenMan";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    thisCharacter.GetComponent<Player_Controller>().enabled = true;
                    otherCharacter.GetComponent<Player_Controller>().enabled = false;
                    
                    Rigidbody2D otherCharactersRigidBody = otherCharacter.GetComponent<Rigidbody2D>();
                    otherCharactersRigidBody.gravityScale = 25f;
                    
                    Animator otherCharactersAnimator = otherCharacter.GetComponent<Animator>();
                    otherCharactersAnimator.SetFloat("Speed", 0);
                    
                    activeCharacter = thisCharacter.transform;
                    
                    foreach (Renderer renderer in activeCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder += 10;
                    }
                    
                    foreach (Renderer renderer in otherCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder -= 10;
                    }
                    
                    thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
                    otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, 0f);
                    //otherCharacter.GetComponent<Character_Switch>().enabled = false;
                }
                else
                {
                    //Debug.Log("Four");
                    if (Environment_Handler.evironmentHandlerInstance != null)
                        Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();

                    currentCharacter = "WoodenMan";
                    otherCharacter.GetComponent<Character_Switch>().activeCharacter = otherCharacter.transform;
                    otherCharacter.GetComponent<Player_Controller>().enabled = true;
                    thisCharacter.GetComponent<Player_Controller>().enabled = false;
                    
                    Rigidbody2D thisCharactersRigidBody = thisCharacter.GetComponent<Rigidbody2D>();
                    thisCharactersRigidBody.gravityScale = 25f;
                    
                    Animator thisCharactersAnimator = thisCharacter.GetComponent<Animator>();
                    thisCharactersAnimator.SetFloat("Speed", 0);
                    
                    activeCharacter = otherCharacter.transform;
                    
                    foreach (Renderer renderer in activeCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder += 10;
                    }
                    
                    foreach (Renderer renderer in thisCharacter.GetComponentsInChildren<Renderer>())
                    {
                        renderer.sortingOrder -= 10;
                    }
                    
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
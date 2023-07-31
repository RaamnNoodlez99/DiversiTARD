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

    private void Awake()
    {
        if (thisCharacter.CompareTag("WoodenMan"))
        {
            Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();

            thisCharacter.GetComponent<Player_Controller>().enabled = true;
            otherCharacter.GetComponent<Player_Controller>().enabled = false;
            activeCharacter = thisCharacter.transform;
            cinemachine.LookAt = activeCharacter;
            cinemachine.Follow = activeCharacter;
            thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
        }

        //ignore collisions between ghost and dad

        Physics2D.IgnoreCollision(thisCharacter.GetComponent<CapsuleCollider2D>(), otherCharacter.GetComponent<CapsuleCollider2D>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeCharacter != null)
            {
                if (activeCharacter.CompareTag("WoodenMan"))
                {
                    if (thisCharacter.CompareTag("Ghost"))
                    {
                        thisCharacter.GetComponent<Player_Controller>().enabled = true;
                        otherCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = thisCharacter.transform;
                        thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
                        otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, 0f);
                        thisCharacter.GetComponent<PlayerInput>().enabled = true;
                        otherCharacter.GetComponent<PlayerInput>().enabled = false;
                    }
                    else
                    {
                        Environment_Handler.evironmentHandlerInstance.spawnGhostEnvironment();

                        otherCharacter.GetComponent<Player_Controller>().enabled = true;
                        thisCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = otherCharacter.transform;
                        otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, -0.1f);
                        thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, 0f);
                        otherCharacter.GetComponent<PlayerInput>().enabled = true;
                        thisCharacter.GetComponent<PlayerInput>().enabled = false;
                    }
                }
                else if (activeCharacter.CompareTag("Ghost"))
                {
                    if (thisCharacter.CompareTag("WoodenMan"))
                    {
                        Environment_Handler.evironmentHandlerInstance.spawnFatherEnvironment();

                        thisCharacter.GetComponent<Player_Controller>().enabled = true;
                        otherCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = thisCharacter.transform;
                        thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, -0.1f);
                        otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, 0f);
                        thisCharacter.GetComponent<PlayerInput>().enabled = true;
                        otherCharacter.GetComponent<PlayerInput>().enabled = false;
                    }
                    else
                    {
                        otherCharacter.GetComponent<Player_Controller>().enabled = true;
                        thisCharacter.GetComponent<Player_Controller>().enabled = false;
                        activeCharacter = otherCharacter.transform;
                        otherCharacter.transform.position = new Vector3(otherCharacter.transform.position.x, otherCharacter.transform.position.y, -0.1f);
                        thisCharacter.transform.position = new Vector3(thisCharacter.transform.position.x, thisCharacter.transform.position.y, 0f);
                        otherCharacter.GetComponent<PlayerInput>().enabled = true;
                        thisCharacter.GetComponent<PlayerInput>().enabled = false;
                    }
                }

                cinemachine.LookAt = activeCharacter;
                cinemachine.Follow = activeCharacter;
            }
        }
    }
}

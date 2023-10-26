using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Door : MonoBehaviour
{
    public bool isDadDoor;
    public bool isGhostDoor;
    public GameObject connectedDadDoor;
    public GameObject connectedGhostDoor;
    public GameObject dad;
    public GameObject ghost;
    public SpriteRenderer doorRenderer;
    public Sprite ghostSprite;
    public Sprite dadSprite;
    public bool spawnToTheLeft = false;
    public GameObject hint;
    private bool isTeleporting = false;
    public float teleportTimer = 180f;
    private bool showHint = false;
    public float timeInPortal = 1f;

    private Animator childAnimator;
    public GameObject [] linkedTeleports;
    private Animator [] linkedTeleportAnimators;


    private bool qeueDeActivation = false;

    void Awake()
    {
        // if (isDadDoor && dad.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
        // {
        //     gameObject.SetActive(true);
        // }
        // else if(isGhostDoor && !isDadDoor)
        // {
        //     gameObject.SetActive(false);
        // }
        
        childAnimator = GetComponentInChildren<Animator>();

        linkedTeleportAnimators = new Animator[linkedTeleports.Length];
     
        for (int i = 0; i < linkedTeleports.Length; i++)
        {
            linkedTeleportAnimators[i] = linkedTeleports[i].GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {

        if (teleportTimer > 0)
        {
            teleportTimer -= Time.deltaTime;

            if (teleportTimer <= 0)
            {
                showHint = true;
            }
        }

        if (isTeleporting && showHint)
        {
            if(hint != null)
                hint.SetActive(true);
        }
        else
        {
            if (hint != null)
                hint.SetActive(false);
        }

       /* if (qeueDeActivation)
        {
            if (!isTeleporting)
            {
                qeueDeActivation = false;
                gameObject.SetActive(false);
            }
        }

        if (dad.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
        {
            //doorRenderer.sprite = dadSprite;
        }
        else
        {
            //doorRenderer.sprite = ghostSprite;
        }*/
    }

    public void SetToGhostDoor()
    {
        if(isDadDoor && !isGhostDoor)
        {
            if (!isTeleporting)
                gameObject.SetActive(false);
            else
                qeueDeActivation = true;

        }
        else if(isGhostDoor)
        {
            gameObject.SetActive(true);
        }
    }

    public void SetToDadDoor()
    {
        if (isDadDoor)
        {
            gameObject.SetActive(true);
        }
        else if(isGhostDoor && !isDadDoor)
        {
            if(!isTeleporting)
                gameObject.SetActive(false);
            else
                qeueDeActivation = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTeleporting && (collision.CompareTag("WoodenMan") || collision.CompareTag("Ghost")) || collision.CompareTag("Boss"))
        {
            StartCoroutine(TeleportCharacter(collision.gameObject));
        }
    }

    IEnumerator TeleportCharacter(GameObject character)
    {
        //Begin teleporting Animation

        if (!isTeleporting && !character.GetComponent<Player_Controller>().IsBusyTeleporting)
        {
            isTeleporting = true;
            if (childAnimator != null)
            {
                childAnimator.SetBool("startTeleport", true);
            }

            if (linkedTeleportAnimators != null && linkedTeleportAnimators.Length > 0)
            {
                for (int i = 0; i < linkedTeleportAnimators.Length; i++)
                {
                    linkedTeleportAnimators[i].SetBool("startTeleport", true);
                }
            }


            string exitDirection = "right";

            if (character.CompareTag("Boss"))
            {
                character.GetComponent<Boss_Phase3>().HasTelported();

                if (character.GetComponent<Boss_Phase3>().direction == "left")
                {
                    exitDirection = "right";
                }
                else
                {
                    exitDirection = "left";
                }
            }


            if (character.CompareTag("WoodenMan") || character.CompareTag("Ghost"))
            {
                character.GetComponent<Player_Controller>().IsBusyTeleporting = true;

                if (character.GetComponent<Player_Controller>().facingLeft)
                {
                    exitDirection = "left";
                }
                else
                {
                    exitDirection = "right";
                }
            }

            if (SFX_Manager.sfxInstance.teleport != null )
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.teleport);

            DisableRenderers(character.transform);

            if (character.CompareTag("WoodenMan") || character.CompareTag("Ghost"))
            {
                if (timeInPortal > 0)
                    StartCoroutine(character.GetComponent<Player_Controller>().FreezeMovementInputForDuration(timeInPortal));
            }

            yield return new WaitForSeconds(timeInPortal);

            Vector3 teleportPosition;

            if (dad.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                if (exitDirection == "right")
                {
                    teleportPosition = connectedDadDoor.transform.position + new Vector3(7f, 0f, 0f);
                }
                else
                {
                    teleportPosition = connectedDadDoor.transform.position + new Vector3(-7f, 0f, 0f);
                }
            }
            else
            {
                if (exitDirection == "right")
                {
                    teleportPosition = connectedGhostDoor.transform.position + new Vector3(7f, 0f, 0f);
                }
                else
                {
                    teleportPosition = connectedGhostDoor.transform.position + new Vector3(-7f, 0f, 0f);
                }
            }

            character.transform.position = teleportPosition;


            EnableRenderers(character.transform);

            Debug.Log("here");

            //Finish teleporting animation
            if (childAnimator != null)
            {
                childAnimator.SetBool("startTeleport", false);
            }

            if (linkedTeleportAnimators != null && linkedTeleportAnimators.Length > 0)
            {
                for (int i = 0; i < linkedTeleportAnimators.Length; i++)
                {
                    linkedTeleportAnimators[i].SetBool("startTeleport", false);
                }
            }

            isTeleporting = false;
        }
    }

    private void DisableRenderers(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = false;
            }

            DisableRenderers(child);
        }
    }

    private void EnableRenderers(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = true;
            }

            EnableRenderers(child);
        }
    }

}

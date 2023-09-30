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
    private bool isTeleporting = false;

    private bool qeueDeActivation = false;

    void Awake()
    {
        if (isDadDoor && dad.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
        {
            gameObject.SetActive(true);
        }
        else if(isGhostDoor && !isDadDoor)
        {
            gameObject.SetActive(false);
        }
        else if (isDadDoor)
        {
            //Swap sprites
        }

    }

    void Update()
    {
        if (qeueDeActivation)
        {
            if (!isTeleporting)
            {
                qeueDeActivation = false;
                gameObject.SetActive(false);
            }
        }
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
            //SwapSprites
        }
    }

    public void SetToDadDoor()
    {
        if (isDadDoor)
        {
            gameObject.SetActive(true);
            //SwapSprites
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
        if (!isTeleporting && (collision.CompareTag("WoodenMan") || collision.CompareTag("Ghost")))
        {
            StartCoroutine(TeleportCharacter(collision.gameObject));
        }
    }

    IEnumerator TeleportCharacter(GameObject character)
    {
        isTeleporting = true;
        character.GetComponent<Player_Controller>().IsBusyTeleporting = true;

        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.teleport);
        DisableRenderers(character.transform);

        StartCoroutine(character.GetComponent<Player_Controller>().FreezeMovementInputForDuration(1f));
        yield return new WaitForSeconds(1.0f);

        Vector3 teleportPosition;

        if (dad.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
        {
            teleportPosition = connectedDadDoor.transform.position + new Vector3(7f, 0f, 0f); 
        }
        else
        {
            teleportPosition = connectedGhostDoor.transform.position + new Vector3(7f, 0f, 0f); 
        }

        character.transform.position = teleportPosition;


        EnableRenderers(character.transform); isTeleporting = false;

        yield return new WaitForSeconds(0f);
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

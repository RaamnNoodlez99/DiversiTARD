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

        StartCoroutine(character.GetComponent<Player_Controller>().FreezeMovementInputForDuration(1f));
        yield return new WaitForSeconds(1.0f);

        Vector3 teleportPosition;

        if (dad.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
        {
            // Teleport the character to the right of the connected dad door
            teleportPosition = connectedDadDoor.transform.position + new Vector3(10f, 0f, 0f); // Adjust the values as needed
        }
        else
        {
            // Teleport the character to the right of the connected ghost door
            teleportPosition = connectedGhostDoor.transform.position + new Vector3(10f, 0f, 0f); // Adjust the values as needed
        }

        character.transform.position = teleportPosition;

        isTeleporting = false;

        yield return new WaitForSeconds(0f);
    }

}

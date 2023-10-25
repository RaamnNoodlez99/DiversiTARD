using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{

    private Character_Switch characterSwitch;
    private GameObject[] teleportDads;
    private GameObject[] teleportGhosts;
    void Start()
    {
        GameObject woodenMan = GameObject.FindGameObjectWithTag("WoodenMan");
        characterSwitch = woodenMan.GetComponent<Character_Switch>();
        
        teleportDads = GameObject.FindGameObjectsWithTag("TeleportDad");
        teleportGhosts = GameObject.FindGameObjectsWithTag("TeleportGhost");
    }


    void Update()
    {
        if (characterSwitch.getCurCharacter() == "WoodenMan")
        {
 
            foreach (GameObject dadTP in teleportDads)
            {
                //dadTP.SetActive(true);
                EnableRenderers(dadTP.transform);
                BoxCollider2D teleportBoxCollider = dadTP.GetComponent<BoxCollider2D>();
                teleportBoxCollider.enabled = true;
            }

            foreach (GameObject ghostTP in teleportGhosts)
            {
                //ghostTP.SetActive(false);
                DisableRenderers(ghostTP.transform);
                BoxCollider2D teleportBoxCollider = ghostTP.GetComponent<BoxCollider2D>();
                teleportBoxCollider.enabled = false;
            }
        }
        else
        {
            foreach (GameObject dadTP in teleportDads)
            {
                //dadTP.SetActive(false);
                DisableRenderers(dadTP.transform);
                BoxCollider2D teleportBoxCollider = dadTP.GetComponent<BoxCollider2D>();
                teleportBoxCollider.enabled = false;
            }

            foreach (GameObject ghostTP in teleportGhosts)
            {
                //ghostTP.SetActive(true);
                EnableRenderers(ghostTP.transform);
                BoxCollider2D teleportBoxCollider = ghostTP.GetComponent<BoxCollider2D>();
                teleportBoxCollider.enabled = true;
            }
        }
    }

    private void DisableRenderers(Transform parentTransform)
    {
        Renderer parentRenderer = parentTransform.GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            parentRenderer.enabled = false;
        }

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
        Renderer parentRenderer = parentTransform.GetComponent<Renderer>();
        if (parentRenderer != null)
        {
            parentRenderer.enabled = true;
        }

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


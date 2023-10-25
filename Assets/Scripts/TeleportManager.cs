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
                dadTP.SetActive(true);
            }
            
            foreach (GameObject ghostTP in teleportGhosts)
            {
                ghostTP.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject dadTP in teleportDads)
            {
                dadTP.SetActive(false);
            }
            
            foreach (GameObject ghostTP in teleportGhosts)
            {
                ghostTP.SetActive(true);
            }
        }
    }
}


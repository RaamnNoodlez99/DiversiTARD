using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Handler : MonoBehaviour
{
    //public GameObject ghostFloor;
    public GameObject ghostBackground;
    //public GameObject fatherFloor;
    public GameObject fatherBackground;
    bool isFatherEnvironment = true;

    public float floorX = 2;
    public float floorY = -47;

    public float backGroundX = 0;
    public float backGroundY = 0;

    private GameObject currentFatherFloor;
    private GameObject currentGhostFloor;
    private GameObject currentFatherBackGround;
    private GameObject currentGhostBackGround;
    public bool switchOff = true;
    public bool shouldLoadPlatforms = false;

    public static Environment_Handler evironmentHandlerInstance;

    private void Update()
    {
        if(currentFatherBackGround != null)
        {
            if (currentFatherBackGround.transform.Find("Switches").gameObject.GetComponent<Switch>().isSwitchedOn)
            {
                switchOff = false;
                shouldLoadPlatforms = true;
            }
        }
    }
    void Awake()
    {
        if (evironmentHandlerInstance != null && evironmentHandlerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        evironmentHandlerInstance = this;
        DontDestroyOnLoad(this);
    }

    public void spawnGhostEnvironment()
    {
        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.environmentChange);

        if (isFatherEnvironment)
        {
            Destroy(currentFatherBackGround);
            currentFatherBackGround = null;
        }

        if (currentGhostBackGround == null)
        {
            Vector3 spawnPosition = new Vector3(backGroundX, backGroundY, 0f);
            currentGhostBackGround = Instantiate(ghostBackground, spawnPosition, Quaternion.identity);
        }

        isFatherEnvironment = false;
        if (shouldLoadPlatforms)
        {
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut1").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut2").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut3").gameObject.SetActive(true);
        }
        if (switchOff)
        {
            currentGhostBackGround.transform.Find("Switches/Ghost switch Off").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("Switches/Ghost switch On").gameObject.SetActive(false);
        }
        else
        {
            currentGhostBackGround.transform.Find("Switches/Ghost switch On").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("Switches/Ghost switch Off").gameObject.SetActive(false);
        }
    }

    public void spawnFatherEnvironment()
    {
        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.environmentChange);

        if (!isFatherEnvironment)
        {
            Destroy(currentGhostBackGround);
            currentGhostBackGround = null;
        }


        if (currentFatherBackGround == null)
        {
            Vector3 spawnPosition = new Vector3(backGroundX, backGroundY, 0f);
            currentFatherBackGround = Instantiate(fatherBackground, spawnPosition, Quaternion.identity);
        }

        isFatherEnvironment = true;

        if (shouldLoadPlatforms)
        {
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut1").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut2").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut3").gameObject.SetActive(true);
        }
        if (switchOff)
        {
            currentFatherBackGround.transform.Find("Switches/Ghost switch Off").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("Switches/Ghost switch On").gameObject.SetActive(false);
        }
        else
        {
            currentFatherBackGround.transform.Find("Switches/Ghost switch On").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("Switches/Ghost switch Off").gameObject.SetActive(false);
        }
    }
}

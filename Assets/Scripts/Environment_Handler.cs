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
    public List<GameObject> brittlePlatforms = new List<GameObject>();
    public float backGroundX = 0;
    public float backGroundY = 0;
    public bool lookForSwitch = false;
    public bool lookForTutPlatforms = false;

    private GameObject cuFloorrrentFather;
    private GameObject currentGhostFloor;
    private GameObject currentFatherBackGround;
    private GameObject currentGhostBackGround;
    public bool switchOff = true;
    public bool shouldLoadPlatforms = false;

    public static Environment_Handler evironmentHandlerInstance;

    private void Update()
    {
        if(currentFatherBackGround != null && lookForSwitch)
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
        Cursor.visible = false;
        if (evironmentHandlerInstance != null && evironmentHandlerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        evironmentHandlerInstance = this;
        //DontDestroyOnLoad(this);
    }

    public void spawnGhostEnvironment()
    {
        foreach(GameObject brittlePlatform in brittlePlatforms)
        {
            brittlePlatform.GetComponent<Platform_Life>().LoseHealth(1);
        }

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
        if (shouldLoadPlatforms && lookForTutPlatforms)
        {
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut1").gameObject.transform.Translate(0f, -9f, 0f);
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut2").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut3").gameObject.SetActive(true);
        }
        else if(lookForTutPlatforms)
        {
            //currentGhostBackGround.transform.Find("TutorialPlatforms/Tut1").gameObject.transform.Translate(0f, -9f, 0f);
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut2").gameObject.SetActive(false);
            currentGhostBackGround.transform.Find("TutorialPlatforms/Tut3").gameObject.SetActive(false);
        }

        if (lookForSwitch && switchOff)
        {
            currentGhostBackGround.transform.Find("Ghost_Switch/Ghost switch Off").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("Ghost_Switch/Ghost switch On").gameObject.SetActive(false);
        }
        else if(lookForSwitch)
        {
            currentGhostBackGround.transform.Find("Ghost_Switch/Ghost switch On").gameObject.SetActive(true);
            currentGhostBackGround.transform.Find("Ghost_Switch/Ghost switch Off").gameObject.SetActive(false);
        }
    }

    public void spawnFatherEnvironment()
    {
        foreach (GameObject brittlePlatform in brittlePlatforms)
        {
            brittlePlatform.GetComponent<Platform_Life>().LoseHealth(1);
        }

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

        if (shouldLoadPlatforms && lookForTutPlatforms)
        {
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut1").gameObject.transform.Translate(0f, -9f, 0f);
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut2").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut3").gameObject.SetActive(true);
        }
        else if (lookForTutPlatforms)
        {
            //currentFatherBackGround.transform.Find("TutorialPlatforms/Tut1").gameObject.SetActive(false);
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut2").gameObject.SetActive(false);
            currentFatherBackGround.transform.Find("TutorialPlatforms/Tut3").gameObject.SetActive(false);
        }

        if (switchOff && lookForSwitch)
        {
            currentFatherBackGround.transform.Find("Switches/Ghost switch Off").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("Switches/Ghost switch On").gameObject.SetActive(false);
        }
        else if(lookForSwitch)
        {
            currentFatherBackGround.transform.Find("Switches/Ghost switch On").gameObject.SetActive(true);
            currentFatherBackGround.transform.Find("Switches/Ghost switch Off").gameObject.SetActive(false);
        }
    }
}

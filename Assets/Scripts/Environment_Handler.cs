using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Handler : MonoBehaviour
{
    public GameObject ghostFloor;
    public GameObject ghostBackground;
    public GameObject fatherFloor;
    public GameObject fatherBackground;
    bool isFatherEnvironment = true;

    public float floorX = 2;
    public float floorY = -47;

    public float backGroundX = 2.45f;
    public float backGroundY = 17;

    private GameObject currentFatherFloor;
    private GameObject currentGhostFloor;
    private GameObject currentFatherBackGround;
    private GameObject currentGhostBackGround;

    public static Environment_Handler evironmentHandlerInstance;

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
            Destroy(currentFatherFloor);
            currentFatherFloor = null;
            Destroy(currentFatherBackGround);
            currentFatherBackGround = null;
        }

        if (currentGhostFloor == null)
        {
            Vector3 spawnPosition = new Vector3(floorX, floorY, 0f);
            currentGhostFloor = Instantiate(ghostFloor, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned ghost floor");
        }

        if (currentGhostBackGround == null)
        {
            Vector3 spawnPosition = new Vector3(backGroundX, backGroundY + 1.56f, 0f);
            currentGhostBackGround = Instantiate(ghostBackground, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned ghost evironment");
        }

        isFatherEnvironment = false;
    }

    public void spawnFatherEnvironment()
    {
        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.environmentChange);

        if (!isFatherEnvironment)
        {
            Destroy(currentGhostFloor);
            currentGhostFloor = null;
            Destroy(currentGhostBackGround);
            currentGhostBackGround = null;
        }

        if (currentFatherFloor == null)
        {
            Vector3 spawnPosition = new Vector3(floorX, floorY + 3f, 0f);
            currentFatherFloor = Instantiate(fatherFloor, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned father floor");
        }

        if (currentFatherBackGround == null)
        {
            Vector3 spawnPosition = new Vector3(backGroundX, backGroundY, 0f);
            currentFatherBackGround = Instantiate(fatherBackground, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned father evironment");
        }

        isFatherEnvironment = true;
    }
}

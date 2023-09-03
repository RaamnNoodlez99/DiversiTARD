using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeScreenOnTimer : MonoBehaviour
{
    public float changeTime;

    public PlayerInput levelInput;
    public Animator sceneAnimator;

    public GameObject healthbar;
    public GameObject ghostHUD;

    public GameObject actualCamera;
    public GameObject stateMachineToDisable;

    public bool isFallingPlatformsLevel = false;
    public bool isTutorialLevel = false;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasSeenFallingPlatformsCutscene") == 0 && isFallingPlatformsLevel)
        {
            levelInput.enabled = false;
            sceneAnimator.SetBool("startCutscene", true);
            Invoke("StopCutscene", changeTime);
            PlayerPrefs.SetInt("hasSeenFallingPlatformsCutscene", 1);
        } else if (!isFallingPlatformsLevel && !isTutorialLevel)
        {
            levelInput.enabled = false;
            sceneAnimator.SetBool("startCutscene", true);
            Invoke("StopCutscene", changeTime);
        } else if (PlayerPrefs.GetInt("hasSeenFallingPlatformsCutscene") == 1 && isFallingPlatformsLevel)
        {
            healthbar.SetActive(true);
            ghostHUD.SetActive(true);
        }
        
        if (PlayerPrefs.GetInt("hasSeenTutorialCutscene") == 0 && isTutorialLevel)
        {
            levelInput.enabled = false;
            sceneAnimator.SetBool("startCutscene", true);
            Invoke("StopCutscene", changeTime);
            PlayerPrefs.SetInt("hasSeenTutorialCutscene", 1);
        } else if (!isFallingPlatformsLevel && !isTutorialLevel)
        {
            levelInput.enabled = false;
            sceneAnimator.SetBool("startCutscene", true);
            Invoke("StopCutscene", changeTime);
        } else if (PlayerPrefs.GetInt("hasSeenTutorialCutscene") == 1 && isTutorialLevel)
        {
            healthbar.SetActive(true);
            ghostHUD.SetActive(true);
        }
    }

    // private void Update()
    // {
    //     changeTime -= Time.deltaTime;
    //     if(changeTime <= 0)
    //         SceneManager.LoadScene(sceneName);
    // }

    private void StopCutscene()
    {
        levelInput.enabled = true;
        sceneAnimator.SetBool("startCutscene", false);
        healthbar.SetActive(true);
        ghostHUD.SetActive(true);
        stateMachineToDisable.SetActive(false);
        actualCamera.SetActive(true);
    }
}

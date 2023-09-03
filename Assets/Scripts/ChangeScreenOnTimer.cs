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
        // if (PlayerPrefs.GetInt("hasSeenFallingPlatformsCutscene") == 0 && isFallingPlatformsLevel)
        // {
        //     levelInput.enabled = false;
        //     sceneAnimator.SetBool("startCutscene", true);
        //     Invoke("StopCutscene", changeTime);
        //     PlayerPrefs.SetInt("hasSeenFallingPlatformsCutscene", 1);
        // } else if (!isFallingPlatformsLevel && !isTutorialLevel)
        // {
        //     levelInput.enabled = false;
        //     sceneAnimator.SetBool("startCutscene", true);
        //     Invoke("StopCutscene", changeTime);
        // } else if (PlayerPrefs.GetInt("hasSeenFallingPlatformsCutscene") == 1 && isFallingPlatformsLevel)
        // {
        //     healthbar.SetActive(true);
        //     ghostHUD.SetActive(true);
        // }
        //
        // if (PlayerPrefs.GetInt("hasSeenTutorialCutscene") == 0 && isTutorialLevel)
        // {
        //     levelInput.enabled = false;
        //     sceneAnimator.SetBool("startCutscene", true);
        //     Invoke("StopCutscene", changeTime);
        //     PlayerPrefs.SetInt("hasSeenTutorialCutscene", 1);
        // } else if (!isFallingPlatformsLevel && !isTutorialLevel)
        // {
        //     levelInput.enabled = false;
        //     sceneAnimator.SetBool("startCutscene", true);
        //     Invoke("StopCutscene", changeTime);
        // } else if (PlayerPrefs.GetInt("hasSeenTutorialCutscene") == 1 && isTutorialLevel)
        // {hasSeenTutorialCutscene
        //     healthbar.SetActive(true);
        //     ghostHUD.SetActive(true);
        // }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.GetInt("hasSeenTutorialCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenTutorialCutscene", 1);
            }
            else
            {
                healthbar.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (PlayerPrefs.GetInt("hasSeenSingleRailCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenSingleRailCutscene", 1);
            }
            else
            {
                healthbar.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (PlayerPrefs.GetInt("hasSeenMultipleRailCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenMultipleRailCutscene", 1);
            }
            else
            {
                healthbar.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (PlayerPrefs.GetInt("hasSeenFallingPlatformsCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenFallingPlatformsCutscene", 1);
            }
            else
            {
                healthbar.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
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

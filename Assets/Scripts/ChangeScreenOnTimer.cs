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
    public GameObject lives;

    public GameObject actualCamera;
    public GameObject stateMachineToDisable;

    public bool isFallingPlatformsLevel = false;
    public bool isTutorialLevel = false;
    private void Awake()
    {

        if (SceneManager.GetActiveScene().buildIndex == 2)
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
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 3)
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
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 4)
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
               // healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 5)
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
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (PlayerPrefs.GetInt("hasSeenSingleTeleportCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenSingleTeleportCutscene", 1);
            }
            else
            {
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            if (PlayerPrefs.GetInt("hasSeenAlternatingWallsCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenAlternatingWallsCutscene", 1);
            }
            else
            {
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            if (PlayerPrefs.GetInt("hasSeenDamageRailsCutscene") == 0)
            {
                levelInput.enabled = false;
                sceneAnimator.SetBool("startCutscene", true);
                Invoke("StopCutscene", changeTime);
                PlayerPrefs.SetInt("hasSeenDamageRailsCutscene", 1);
            }
            else
            {
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 9)
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
                //healthbar.SetActive(true);
                lives.SetActive(true);
                ghostHUD.SetActive(true);
                stateMachineToDisable.SetActive(false);
                actualCamera.SetActive(true);
            }
        }
    }

    private void StopCutscene()
    {
        levelInput.enabled = true;
        sceneAnimator.SetBool("startCutscene", false);
        healthbar.SetActive(true);
        lives.SetActive(true);
        ghostHUD.SetActive(true);
        stateMachineToDisable.SetActive(false);
        actualCamera.SetActive(true);
    }
}

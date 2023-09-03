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
    private void Awake()
    {
        levelInput.enabled = false;
        sceneAnimator.SetBool("startCutscene", true);
        Invoke("StopCutscene", changeTime);
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

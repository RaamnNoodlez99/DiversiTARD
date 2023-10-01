using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject Phase1StartScript;
    public GameObject[] backgroundAudios;
    public AudioSource bossRoar;

    private Player_Controller dadPlayerController;
    private Player_Controller ghostPlayerController;
    private void Awake()
    {
        dadPlayerController = GameObject.FindWithTag("WoodenMan").GetComponent<Player_Controller>();
        ghostPlayerController = GameObject.FindWithTag("Ghost").GetComponent<Player_Controller>();
        Phase1StartScript.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") || other.CompareTag("Ghost"))
        {
            bossRoar.Play();
            dadPlayerController.inputManager.SetActive(false);
            ghostPlayerController.inputManager.SetActive(false);
            cutsceneAnimator.SetBool("cutscene1", true);
            Phase1StartScript.SetActive(true);
            Invoke("stopCutscene", 3f);
        }
    }

    void stopCutscene()
    {
        dadPlayerController.inputManager.SetActive(true);
        ghostPlayerController.inputManager.SetActive(true);
        cutsceneAnimator.SetBool("cutscene1", false);

        foreach(GameObject source in backgroundAudios)
        {
            source.GetComponent<AudioSource>().Play();
        }

        Destroy(gameObject);
    }
}

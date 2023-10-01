using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject Phase1StartScript;

    private Player_Controller dadPlayerController;
    private Player_Controller ghostPlayerController;

    public bool loadNextScene = false;
    private void Awake()
    {
        dadPlayerController = GameObject.FindWithTag("WoodenMan").GetComponent<Player_Controller>();
        ghostPlayerController = GameObject.FindWithTag("Ghost").GetComponent<Player_Controller>();
        if(Phase1StartScript != null)
            Phase1StartScript.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") || other.CompareTag("Ghost"))
        {
            dadPlayerController.inputManager.SetActive(false);
            ghostPlayerController.inputManager.SetActive(false);
            if(Phase1StartScript != null)
                Phase1StartScript.SetActive(true);

            if (loadNextScene)
            {
                cutsceneAnimator.SetBool("startCutscene", true);
                Invoke("loadNextLevel", 3f);
            }
            else
            {
                cutsceneAnimator.SetBool("cutscene1", true);
                Invoke("stopCutscene", 3f);
            }
        }
    }

    void loadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void stopCutscene()
    {
        dadPlayerController.inputManager.SetActive(true);
        ghostPlayerController.inputManager.SetActive(true);
        cutsceneAnimator.SetBool("cutscene1", false);
        Destroy(gameObject);
    }
}

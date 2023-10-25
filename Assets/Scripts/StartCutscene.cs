using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutscene : MonoBehaviour
{
    public Animator cutsceneAnimator;
    public GameObject Phase1StartScript;
    public GameObject[] backgroundAudios;
    public AudioSource bossRoar;

    private Player_Controller dadPlayerController;
    private Player_Controller ghostPlayerController;

    public bool loadNextScene = false;
    private bool bossSoundPlayed = false;

    private bool isStartPhase = false;
    private void Start()
    {
        dadPlayerController = GameObject.FindWithTag("WoodenMan").GetComponent<Player_Controller>();
        ghostPlayerController = GameObject.FindWithTag("Ghost").GetComponent<Player_Controller>();
        if(Phase1StartScript != null)
            Phase1StartScript.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            if (PlayerPrefs.GetInt("hasSeenBossLevelCutscene") == 0)
            {

                if (other.CompareTag("WoodenMan") || other.CompareTag("Ghost"))
                {
                    if(bossRoar != null)
                        bossRoar.Play();
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
                
                if (Phase1StartScript != null)
                {
                    if (!isStartPhase)
                    {
                        PlayerPrefs.SetInt("hasSeenBossLevelCutscene", 1);
                        Phase1StartScript.SetActive(true);
                        isStartPhase = true;
                    }
                }
            }
            else
            {
                if (bossRoar != null && !bossSoundPlayed)
                {
                    bossSoundPlayed = true;
                    bossRoar.Play();
                }
                
                cutsceneAnimator.SetBool("startLevelCamera", true);

                foreach(GameObject source in backgroundAudios)
                {
                    source.GetComponent<AudioSource>().Play();
                }

                if (Phase1StartScript != null)
                {
                    if (!isStartPhase)
                    {
                        StartCoroutine(Phase1Effect(25f, 3f));
                        isStartPhase = true;
                    }
                }

            }
        } else if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            if (loadNextScene)
            {
                loadNextLevel();
            }
        }
        

        
    }
    
    IEnumerator Phase1Effect(float newSpeed, float duration)
    {
        if (Phase1StartScript != null)
        {
            Phase1StartScript.SetActive(true);
            Phase1StartScript.GetComponent<Phase1>().monsterSpeed = newSpeed;

            yield return new WaitForSeconds(duration);

            Phase1StartScript.GetComponent<Phase1>().monsterSpeed = 10f;
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

        foreach(GameObject source in backgroundAudios)
        {
            source.GetComponent<AudioSource>().Play();
        }

        Destroy(gameObject);
    }

    void resumePlayerControls ()
    {
        Destroy(gameObject);
        dadPlayerController.inputManager.SetActive(true);
        ghostPlayerController.inputManager.SetActive(true);
    }
    
}

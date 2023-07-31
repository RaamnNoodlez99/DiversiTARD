using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        SFX_Manager.sfxInstance.BackgroundAudio.Pause();
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SFX_Manager.sfxInstance.BackgroundAudio.PlayOneShot(SFX_Manager.sfxInstance.tutorialBackgroundMusic);
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void ToMenu()
    {
        SFX_Manager.sfxInstance.BackgroundAudio.Stop();
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public static bool isPaused;
    public float VolumeChangeDuration = 1.0f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;


    void Start()
    {
        initialVolume = SFX_Manager.sfxInstance.BackgroundAudio.volume;
        pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                Debug.Log("piel");
                ResumeGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("drol");
                ResumeGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                Debug.Log("piel2");
                PauseGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("drol2");
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (!Game_Over.isOver)
        {
            volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, initialVolume, 0.13f));

            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (volumeChangeCoroutine != null)
        {
            StopCoroutine(volumeChangeCoroutine);
        }

        volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, SFX_Manager.sfxInstance.BackgroundAudio.volume, initialVolume)); 
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void ToMenu()
    {
        SFX_Manager.sfxInstance.BackgroundAudio.volume = 1f;
        SFX_Manager.sfxInstance.BackgroundAudio.Stop();
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    private IEnumerator ChangeVolumeOverTime(float duration, float startVolume, float targetVolume)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, t);
            SFX_Manager.sfxInstance.BackgroundAudio.volume = newVolume;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        SFX_Manager.sfxInstance.BackgroundAudio.volume = targetVolume;
    }
}

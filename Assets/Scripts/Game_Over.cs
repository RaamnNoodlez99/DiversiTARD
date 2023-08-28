using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game_Over : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject environmentHandler;
    public GameObject woodenMan;
    public GameObject ghost;
    public static bool isOver;
    public GameObject firstSelectedButton;

    public float VolumeChangeDuration = 1.0f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;

    void Start()
    {
        initialVolume = SFX_Manager.sfxInstance.BackgroundAudio.volume;
        gameOverScreen.SetActive(false);
    }

    public bool isGameOver()
    {
        return isOver;
    }

    private void Update()
    {
        if (woodenMan == null || ghost == null)
        {
            Invoke("GameOver", 1.0f);
        }
    }

    public void GameOver()
    {
        volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, AudioListener.volume, 0.13f));
        gameOverScreen.SetActive(true);
        isOver = true;
        Time.timeScale = 0f;

        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }

    public void ResetGame()
    {
        AudioListener.volume = 1f;
        SFX_Manager.sfxInstance.BackgroundAudio.Play();
        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        gameOverScreen.SetActive(false);
        isOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ToMenu()
    {
        AudioListener.volume = 1f;
        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        SFX_Manager.sfxInstance.BackgroundAudio.volume = 1f;
        SFX_Manager.sfxInstance.BackgroundAudio.Stop();
        isOver = false;
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
            AudioListener.volume = newVolume;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        AudioListener.volume = targetVolume;
    }
}

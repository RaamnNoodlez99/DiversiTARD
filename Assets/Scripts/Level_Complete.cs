using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level_Complete : MonoBehaviour
{
    public bool ghostRequired = false;
    public bool fatherRequired = true;
    public GameObject environmentHandler;
    public GameObject levelOverScreen;
    public GameObject player;
    public static bool levelIsOver;
    public GameObject firstSelectedButton;

    public float VolumeChangeDuration = 1.0f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;

    void Start()
    {
        initialVolume = SFX_Manager.sfxInstance.BackgroundAudio.volume;
        levelOverScreen.SetActive(false);
    }

    public bool isLevelOver()
    {
        return levelIsOver;
    }

    public void LevelOver()
    {
        volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, AudioListener.volume, 0.13f));
        SFX_Manager.sfxInstance.BackgroundAudio.Stop();
        levelOverScreen.SetActive(true);
        levelIsOver = true;
        Time.timeScale = 0f;

        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }

    public void RestartLevel()
    {
        AudioListener.volume = 1f;
        SFX_Manager.sfxInstance.BackgroundAudio.Play();
        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        levelOverScreen.SetActive(false);
        levelIsOver = false;
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
        levelIsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost") && ghostRequired)
        {
            LevelOver();
        }
        if (collision.gameObject.CompareTag("WoodenMan") && fatherRequired)
        {
            LevelOver();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    public AudioSource audioSource;
    public AudioClip levelComplete;

    public AudioMixer audioMixer;


    public float VolumeChangeDuration = 1.0f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;

    void Start()
    {
        initialVolume = SFX_Manager.sfxInstance.Audio.volume;
    }

    public bool isLevelOver()
    {
        return levelIsOver;
    }

    public void LevelOver()
    {
        Cursor.visible = true;
        //SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.LevelComplete);
        audioSource.PlayOneShot(levelComplete,0.5f);
        volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, PlayerPrefs.GetFloat("backgroundVolume"), 0.13f));
        SFX_Manager.sfxInstance.Audio.Stop();
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
        if (volumeChangeCoroutine != null)
            StopCoroutine(volumeChangeCoroutine);

        audioMixer.SetFloat("BackgroundVolume", ConvertToDecibel(PlayerPrefs.GetFloat("backgroundVolume")));
        audioMixer.SetFloat("MasterVolume", ConvertToDecibel(PlayerPrefs.GetFloat("masterVolume")));
        audioMixer.SetFloat("SoundEffectsVolume", ConvertToDecibel(PlayerPrefs.GetFloat("soundEffectsVolume")));

        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        levelOverScreen.SetActive(false);
        levelIsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
        if (volumeChangeCoroutine != null)
            StopCoroutine(volumeChangeCoroutine);

        audioMixer.SetFloat("BackgroundVolume", ConvertToDecibel(PlayerPrefs.GetFloat("backgroundVolume")));
        audioMixer.SetFloat("MasterVolume", ConvertToDecibel(PlayerPrefs.GetFloat("masterVolume")));
        audioMixer.SetFloat("SoundEffectsVolume", ConvertToDecibel(PlayerPrefs.GetFloat("soundEffectsVolume"))); environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        //SFX_Manager.sfxInstance.Audio.volume = 1f;
        //SFX_Manager.sfxInstance.Audio.Stop();
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

            float decibelVolume = ConvertToDecibel(newVolume);

            audioMixer.SetFloat("BackgroundVolume", decibelVolume);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private float ConvertToDecibel(float linearValue)
    {
        return 20f * Mathf.Log10(linearValue);
    }

}

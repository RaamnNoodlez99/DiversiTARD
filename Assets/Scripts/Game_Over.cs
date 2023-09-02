using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Game_Over : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject environmentHandler;
    public GameObject woodenMan;
    public GameObject ghost;
    public static bool isOver;
    public GameObject firstSelectedButton;
    public AudioSource audioSource;
    public AudioClip gameOver;

    private float VolumeChangeDuration = 2.7f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;
    public AudioMixer audioMixer;

    void Start()
    {
        initialVolume = SFX_Manager.sfxInstance.Audio.volume;
    }

    public bool isGameOver()
    {
        return isOver;
    }

    private void Update()
    {
        if (woodenMan == null || ghost == null)
        {
            //audioSource.PlayOneShot(gameOver, 0.2f);
            Invoke("GameOver", 1.5f);
        }
    }

    public void GameOver()
    {
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
        audioSource.Play();
        isOver = true;
        volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, PlayerPrefs.GetFloat("backgroundVolume"), 0f));

        Time.timeScale = 0f;

        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }

    public void ResetGame()
    {
        if (volumeChangeCoroutine != null)
            StopCoroutine(volumeChangeCoroutine);

        audioMixer.SetFloat("BackgroundVolume", ConvertToDecibel(PlayerPrefs.GetFloat("backgroundVolume")));
        audioMixer.SetFloat("MasterVolume", ConvertToDecibel(PlayerPrefs.GetFloat("masterVolume")));
        audioMixer.SetFloat("SoundEffectsVolume", ConvertToDecibel(PlayerPrefs.GetFloat("soundEffectsVolume")));
        
        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        isOver = false;
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
        if (volumeChangeCoroutine != null)
            StopCoroutine(volumeChangeCoroutine);

        audioMixer.SetFloat("BackgroundVolume", ConvertToDecibel(PlayerPrefs.GetFloat("backgroundVolume")));
        audioMixer.SetFloat("MasterVolume", ConvertToDecibel(PlayerPrefs.GetFloat("masterVolume")));
        audioMixer.SetFloat("SoundEffectsVolume", ConvertToDecibel(PlayerPrefs.GetFloat("soundEffectsVolume")));
        
        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        SFX_Manager.sfxInstance.Audio.volume = 1f;
        SFX_Manager.sfxInstance.Audio.Stop();
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

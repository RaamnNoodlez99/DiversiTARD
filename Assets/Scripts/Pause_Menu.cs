using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;


public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameManager;
    public GameObject levelComplete;
    public GameObject characterCheck;
    public GameObject environmentHandler;

    public TextMeshProUGUI characterNameTextMeshPro;
    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;
    public TextMeshProUGUI button3;

    public AudioMixer audioMixer;

    public static bool isPaused;

    public float VolumeChangeDuration = 1.0f;
    public float volumeWhilePaused = 0.7f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;

    void Start()
    {
        initialVolume = SFX_Manager.sfxInstance.Audio.volume;
    }

    public void Update()
    {
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                ResumeGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                PauseGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (!gameManager.GetComponent<Game_Over>().isGameOver() && (levelComplete == null || !levelComplete.GetComponent<Level_Complete>().isLevelOver()))
        {

            if(PlayerPrefs.GetFloat("backgroundVolume") > volumeWhilePaused)
                volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, PlayerPrefs.GetFloat("backgroundVolume"), volumeWhilePaused));

            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
            if(characterCheck.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                Color textColor = ColorUtility.TryParseHtmlString("#2C592E", out Color color) ? color : Color.white;
                characterNameTextMeshPro.color = textColor;
                button1.color = Color.white;
                button2.color = Color.white;
                button2.color = Color.white;
            }
            else
            {
                Color textColor = ColorUtility.TryParseHtmlString("#C5C5C5", out Color color) ? color : Color.white;
                characterNameTextMeshPro.color = textColor;
            }

        }
    }

    public void ResumeGame()
    {

        //volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, 0.1f, PlayerPrefs.GetFloat("backgroundVolume")) );
        
        audioMixer.SetFloat("BackgroundVolume", ConvertToDecibel(PlayerPrefs.GetFloat("backgroundVolume")));
        audioMixer.SetFloat("MasterVolume", ConvertToDecibel(PlayerPrefs.GetFloat("masterVolume")));
        audioMixer.SetFloat("SoundEffectsVolume", ConvertToDecibel(PlayerPrefs.GetFloat("soundEffectsVolume")));

        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void ToMenu()
    {
        if(volumeChangeCoroutine != null)
            StopCoroutine(volumeChangeCoroutine);

        audioMixer.SetFloat("BackgroundVolume", ConvertToDecibel(PlayerPrefs.GetFloat("backgroundVolume")));
        audioMixer.SetFloat("MasterVolume", ConvertToDecibel(PlayerPrefs.GetFloat("masterVolume")));
        audioMixer.SetFloat("SoundEffectsVolume", ConvertToDecibel(PlayerPrefs.GetFloat("soundEffectsVolume")));

        //Switch.GetComponent<Tutorial_Platfrom_Movement>().ResetState();
        environmentHandler.GetComponent<Environment_Handler>().shouldLoadPlatforms = false;
        environmentHandler.GetComponent<Environment_Handler>().switchOff = true;
        //SFX_Manager.sfxInstance.Audio.volume = 1f;
        //SFX_Manager.sfxInstance.Audio.Stop();
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

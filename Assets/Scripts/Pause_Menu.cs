using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;


public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameManager;
    public GameObject levelComplete;
    public GameObject characterCheck;
    public TextMeshProUGUI characterNameTextMeshPro;
    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;
    public TextMeshProUGUI button3;
    public static bool isPaused;
    public float VolumeChangeDuration = 1.0f;
    private float initialVolume;
    private Coroutine volumeChangeCoroutine;
    //GameObject Switch = GameObject.FindWithTag("switch");
    //GameObject tutorialPlatforms = GameObject.FindWithTag("TurorialPlatforms");



    void Start()
    {
        //initialVolume = SFX_Manager.sfxInstance.BackgroundAudio.volume;
        pauseMenu.SetActive(false);
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
        Debug.Log(gameManager.GetComponent<Game_Over>().isGameOver());
        if (!gameManager.GetComponent<Game_Over>().isGameOver() && !levelComplete.GetComponent<Level_Complete>().isLevelOver())
        {
           // Debug.Log("Hello dian");
            //volumeChangeCoroutine = StartCoroutine(ChangeVolumeOverTime(VolumeChangeDuration, initialVolume, 0.13f));

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
        //tutorialPlatforms.GetComponent<Tutorial_Platfrom_Movement>().ResetState();
        //Switch.GetComponent<Tutorial_Platfrom_Movement>().ResetState();
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

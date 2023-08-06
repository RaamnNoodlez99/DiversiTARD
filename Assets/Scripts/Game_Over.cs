using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game_Over : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject player;
    public static bool isOver;
    public GameObject firstSelectedButton; 

    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    public bool isGameOver()
    {
        return isOver;
    }

    private void Update()
    {
        if (player == null)
        {
            Invoke("GameOver", 1.0f);
        }
    }

    public void GameOver()
    {
        SFX_Manager.sfxInstance.BackgroundAudio.Stop();
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
        SFX_Manager.sfxInstance.BackgroundAudio.Play();
        gameOverScreen.SetActive(false);
        isOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ToMenu()
    {
        SFX_Manager.sfxInstance.BackgroundAudio.volume = 1f;
        SFX_Manager.sfxInstance.BackgroundAudio.Stop();
        isOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

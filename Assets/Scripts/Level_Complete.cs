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
    public GameObject levelOverScreen;
    public GameObject player;
    public static bool levelIsOver;
    public GameObject firstSelectedButton;

    void Start()
    {
        levelOverScreen.SetActive(false);
    }

    public bool isLevelOver()
    {
        return levelIsOver;
    }

    public void LevelOver()
    {
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
        SFX_Manager.sfxInstance.BackgroundAudio.Play();
        levelOverScreen.SetActive(false);
        levelIsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
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

}

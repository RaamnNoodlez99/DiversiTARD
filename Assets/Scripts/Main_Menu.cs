using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Menu : MonoBehaviour
{
    private List<Button> childButtons = new List<Button>();
    private float cooldownDuration = 0.1f;
    private bool buttonsRemoved = false;
    public GameObject firstSelectedButton;
    public GameObject removableText;
    public GameObject title;

    private Vector3 initialTitlePosition;
    public float titleMoveSpeed = 10f;
    public float titleHeightIncrease = 350f;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore1") != 1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("hasPlayedBefore1", 1);
        }

        initialTitlePosition = title.transform.position;
        foreach (Button child in GetComponentsInChildren<Button>())
        {
            if (child.CompareTag("Button"))
            {
                childButtons.Add(child);
            }
        }

        RemoveButtons();
    }

    private void Update()
    {
        if (buttonsRemoved)
        {
            if (Input.anyKeyDown)
            {
                Menu_Audio.menuAudioInstance.menuAudio.PlayOneShot(Menu_Audio.menuAudioInstance.buttonPopup);
                StartCoroutine(MoveTitleUp());
            }
        }
    }

    private IEnumerator MoveTitleUp()
    {
        while (title.transform.position.y < initialTitlePosition.y + titleHeightIncrease) 
        {
            title.transform.Translate(Vector3.up * titleMoveSpeed * Time.deltaTime);
            yield return null;
        }

        AddButtons();
    }

    private void RemoveButtons()
    {
        foreach (Button button in childButtons)
        {
            button.gameObject.SetActive(false);
        }
        buttonsRemoved = true;
    }

    private void AddButtons()
    {
        removableText.SetActive(false);
        foreach (Button button in childButtons)
        {
            button.gameObject.SetActive(true);
            button.interactable = false;
        }
        buttonsRemoved = false;
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator ButtonCooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);

        foreach (Button button in childButtons)
            button.interactable = true;

        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void PlayGame()
    {
       // Debug.Log(PlayerPrefs.GetInt("currentLevel"));

        if(PlayerPrefs.GetInt("currentLevel") != 0 || PlayerPrefs.GetInt("currentLevel") == 13)
            SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel"));
        else
            SceneManager.LoadScene(1);
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene(4);
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene(5);
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene(6);
    }
    public void PlayLevel5()
    {
        SceneManager.LoadScene(7);
    }
    public void PlayLevel6()
    {
        SceneManager.LoadScene(8);
    }
    public void PlayLevel7()
    {
        SceneManager.LoadScene(9);
    }
    public void PlayLevel8()
    {
        SceneManager.LoadScene(10);
    }
    public void PlayLevel9()
    {
        SceneManager.LoadScene(11);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

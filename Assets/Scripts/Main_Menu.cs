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
    public float titleHeightIncrease = 100f;

    private void Awake()
    {
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
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

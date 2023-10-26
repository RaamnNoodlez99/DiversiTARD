using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Menu_Manager : MonoBehaviour
{
    public GameObject newGameMainMenu;
    public GameObject newGameMainMButton;
    public GameObject mainMenu;
    public GameObject MainMButton;

    void Start()
    {
        if(PlayerPrefs.GetInt("currentLevel") == 0)
        {
            mainMenu.SetActive(true);
            newGameMainMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(false);
            newGameMainMenu.SetActive(true);
        }
    }

    public void activateMainMenu()
    {
        if (PlayerPrefs.GetInt("currentLevel") == 0)
        {
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(MainMButton);
        }
        else
        {
            newGameMainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(newGameMainMButton);
        }
    }
}

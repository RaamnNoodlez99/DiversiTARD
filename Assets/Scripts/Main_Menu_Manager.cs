using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_Manager : MonoBehaviour
{
    public GameObject newGameMainMenu;
    public GameObject mainMenu;

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
}

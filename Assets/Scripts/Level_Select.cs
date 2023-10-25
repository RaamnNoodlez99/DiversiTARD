using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Level_Select : MonoBehaviour
{
    public GameObject levelButton;
    public AudioClip buttonMove;
    public AudioSource buttonSource;
    public TextMeshProUGUI textMeshComponent;
    public GameObject mainMenu;
    public Image LevelImageComponent;

    public Sprite tutorialImage;
    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Sprite level4;
    public Sprite level5;
    public Sprite level6;
    public Sprite level7;
    public Sprite level8;
    public Sprite level9;


    private Button buttonComponent;
    private bool allowChange = true;
    private int currentLevel;

    private void Start()
    {
        buttonComponent = levelButton.GetComponent<Button>();

        ChangeButton("");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0)
        {
            if (allowChange && EventSystem.current.currentSelectedGameObject == levelButton)
            {
                allowChange = false;
                ChangeButton("left");
                StartCoroutine(InputRefresh());
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0)
        {
            if (allowChange && EventSystem.current.currentSelectedGameObject == levelButton)
            {
                allowChange = false;
                ChangeButton("right");
                StartCoroutine(InputRefresh());
            }
        }

        
    }

    IEnumerator InputRefresh()
    {
        float currentTime = 0.25f; ;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            yield return null;
        }

        allowChange = true;
    }

    private void ChangeButton(string moveDirection)
    {
        buttonSource.PlayOneShot(buttonMove);

        if (moveDirection == "right")
        {
            if (currentLevel == 9)
            {
                currentLevel = 0;
            }
            else
            {
                currentLevel++;
            }
        }
        else if (moveDirection == "left")
        {
            if (currentLevel == 0)
            {
                currentLevel = 9;
            }
            else
            {
                currentLevel--;
            }
        }
        else
        {
            currentLevel = 0;
        }

        switch (currentLevel)
        {
            case 0:
                textMeshComponent.text = "Tutorial";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayTutorial);
                LevelImageComponent.sprite = tutorialImage;
                break;

            case 1:
                textMeshComponent.text = "Level 1";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel1);
                LevelImageComponent.sprite = level1;
                break;

            case 2:
                textMeshComponent.text = "Level 2";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel2);
                LevelImageComponent.sprite = level2;
                break;

            case 3:
                textMeshComponent.text = "Level 3";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel3);
                LevelImageComponent.sprite = level3;
                break;

            case 4:
                textMeshComponent.text = "Level 4";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel4);
                LevelImageComponent.sprite = level4;
                break;

            case 5:
                textMeshComponent.text = "Level 5";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel5);
                LevelImageComponent.sprite = level5;
                break;

            case 6:
                textMeshComponent.text = "Level 6";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel6);
                LevelImageComponent.sprite = level6;
                break;

            case 7:
                textMeshComponent.text = "Level 7";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel7);
                LevelImageComponent.sprite = level7;
                break;

            case 8:
                textMeshComponent.text = "Level 8";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel8);
                LevelImageComponent.sprite = level8;
                break;

            case 9:
                textMeshComponent.text = "Level 9";
                buttonComponent.onClick.RemoveAllListeners();
                buttonComponent.onClick.AddListener(mainMenu.GetComponent<Main_Menu>().PlayLevel9);
                LevelImageComponent.sprite = level9;
                break;

            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open : MonoBehaviour
{
    public GameObject[] linkedTorches;
    public GameObject[] linkedButtons;
    
    public GameObject doorSprite;
    public BoxCollider2D doorCollider;
    public float openSpeed = 10f;
    public float doorLength = 18f;
    public AudioSource stoneInPlace;
    public AudioSource allButtonsDown;


    bool stoneIsInPlace = false;
    private Vector3 initialPosition;
    private Vector3 openPosition;
    private bool havePlayedSound= false;
    private float targetY;


    void Start()
    {
        initialPosition = transform.position;
        openPosition = new Vector3(initialPosition.x, initialPosition.y - doorLength, initialPosition.z);
        targetY = initialPosition.y;
    }
    
    void Update()
    {
        calculateTargetPosition();
        doorSprite.transform.position = Vector3.MoveTowards(doorSprite.transform.position,
            new Vector3(initialPosition.x, targetY, initialPosition.z), openSpeed * Time.deltaTime);

        if (doorSprite.transform.position == openPosition)
        {
            if (!stoneIsInPlace)
            {
                stoneInPlace.Play();
                stoneIsInPlace = true;
            }
            doorCollider.enabled = false;
        }

        bool allButtonsPressed = true;
        if (linkedButtons != null && linkedButtons.Length != 0)
        {
            foreach (var linkedButton in linkedButtons)
            {
                GameObject childButton = linkedButton.transform.GetChild(0).gameObject;
                Button_Red buttonScript = childButton.GetComponent<Button_Red>();

                if (buttonScript.getButtonPressedState() == false)
                {
                    allButtonsPressed = false;
                    break;
                }
            }
        }

        if (linkedButtons != null && linkedButtons.Length != 0)
        {
            if (allButtonsPressed)
            {
                Debug.Log("All buttons Pressed");
                if (!havePlayedSound)
                {
                    havePlayedSound = true;
                    Invoke("playAllButtonsDown", 0.2f);
                }

                foreach (var linkedButton in linkedButtons)
                {
                    GameObject childButton = linkedButton.transform.GetChild(0).gameObject;
                    Button_Red buttonScript = childButton.GetComponent<Button_Red>();
                    buttonScript.allButtonsPressed();
                }
            }
        }
    }


    void playAllButtonsDown()
    {
        allButtonsDown.Play();
    }

    private void calculateTargetPosition()
    {
        int litCount = 0;
        for (int i = 0; i < linkedTorches.Length; i++)
        {
            if (linkedTorches[i].transform.childCount > 0 && linkedTorches[i].transform.GetChild(0).gameObject.activeSelf)
            {
                litCount++;
            }
        }

        float litPercentage = (float)litCount / linkedTorches.Length;
        targetY = Mathf.Lerp(initialPosition.y, openPosition.y, litPercentage);
    }
    
}

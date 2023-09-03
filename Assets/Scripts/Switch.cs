using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public bool isSwitchedOn = false;
    public GameObject offSwitch;
    public GameObject onSwitch;
    public GameObject tutorialPlatforms;
    public GameObject checkCharacter;
    private Button buttonComponent;

    public SpriteRenderer switchOffRenderer;
    public SpriteRenderer switchOnRenderer;

    public Sprite ghostSwitchOff;
    public Sprite ghostSwitchOn;

    public Sprite dadSwitchOff;
    public Sprite dadSwitchOn;

    private bool isDad = true;


    private void Start()
    {
        buttonComponent = GetComponent<Button>();
    }

    private void Update()
    {
        if(checkCharacter != null && checkCharacter.GetComponent<Character_Switch>().getCurCharacter() != "WoodenMan")
        {
            isDad = false;
        }
        else
        {
            isDad = true;
        }

        if (isDad)
        {
            //Debug.Log("Changing To dad switch");
            if(switchOffRenderer != null && dadSwitchOff != null)
            {
                switchOffRenderer.sprite = dadSwitchOff;
            }
            if(switchOnRenderer != null && dadSwitchOn != null)
            {
                switchOnRenderer.sprite = dadSwitchOn;
            }
        }
        else
        {
            //Debug.Log("SwitchONrenderer is null: " + switchOnRenderer == null);
            //Debug.Log("ghostSwitchOn is null: " + ghostSwitchOn == null);

            if (switchOffRenderer != null && ghostSwitchOff != null)
            {
                switchOffRenderer.sprite = ghostSwitchOff;
            }
            if (switchOnRenderer != null && ghostSwitchOn != null)
            {
                switchOnRenderer.sprite = ghostSwitchOn;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("WoodenManAttackArea"))
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.click,1.5f);
            if (isSwitchedOn)
            {
                Off();
            }
            else
            {
                On();
            }
        }
    }

    private void On()
    {
        offSwitch.SetActive(false);
        onSwitch.SetActive(true);
        isSwitchedOn = true;

        if (tutorialPlatforms != null)
        {
            tutorialPlatforms.GetComponent<Tutorial_Platfrom_Movement>().AddPlatforms();
        }
        buttonComponent.onClick.Invoke();
    }

    private void Off()
    {
        offSwitch.SetActive(true);
        onSwitch.SetActive(false);
        isSwitchedOn = false;
        buttonComponent.onClick.Invoke();
    }
}

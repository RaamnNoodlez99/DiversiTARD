using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isSwitchedOn = false;
    public GameObject offSwitch;
    public GameObject onSwitch;
    public GameObject tutorialPlatforms;

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
    }

    private void Off()
    {
        offSwitch.SetActive(true);
        onSwitch.SetActive(false);
        isSwitchedOn = false;
    }
}

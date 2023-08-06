using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Platfrom_Movement : MonoBehaviour
{
    public float platformAddDelay = 1f;
    public GameObject platform;

    public void AddPlatforms()
    {
        if(!platform.activeSelf)
            StartCoroutine(ActivatePlatformsWithDelay(platformAddDelay));
    }

    private IEnumerator ActivatePlatformsWithDelay(float delay)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.stoneShort);
            yield return new WaitForSeconds(delay);
        }
    }

}

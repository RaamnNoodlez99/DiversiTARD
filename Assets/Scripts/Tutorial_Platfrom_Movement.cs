using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Platfrom_Movement : MonoBehaviour
{
    public float platformAddDelay = 2.5f;
    public GameObject mainPlatform;
    public GameObject smallPlatform1;
    public GameObject smallPlatform2;



    public void AddPlatforms()
    {
        if (!smallPlatform1.activeSelf)
        {
            StartCoroutine(MovePlatformDownAndActivatePlatforms());
        }
    }

    private IEnumerator MovePlatformDownAndActivatePlatforms()
    {
        yield return StartCoroutine(MovePlatformDown());

        StartCoroutine(ActivatePlatformsWithDelay(platformAddDelay));
    }

    private IEnumerator ActivatePlatformsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        smallPlatform1.SetActive(true);
        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.stoneShort);

        yield return new WaitForSeconds(delay);
        smallPlatform2.SetActive(true);
        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.stoneShort);
    }

    private IEnumerator MovePlatformDown()
    {
        yield return new WaitForSeconds(0.8f);

        SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.stoneSlide);
        float initialYPosition = mainPlatform.transform.position.y;
        float targetYPosition = initialYPosition - 9f;
        float elapsedTime = 0f;

        while (elapsedTime < 1.8f)
        {
            float newYPosition = Mathf.Lerp(initialYPosition, targetYPosition, elapsedTime / 1.8f);
            mainPlatform.transform.position = new Vector3(mainPlatform.transform.position.x, newYPosition, mainPlatform.transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainPlatform.transform.position = new Vector3(mainPlatform.transform.position.x, targetYPosition, mainPlatform.transform.position.z);
    }



}

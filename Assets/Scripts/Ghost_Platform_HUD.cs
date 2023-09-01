using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost_Platform_HUD : MonoBehaviour
{
    public Image circleFill;
    public float countDownDuration = 6f;
    public Image centerIcon;
    
    private float timer;
    private bool startCountDown = false;

    public void doCountDown()
    {
        startCountDown = true;
    }

    public void pauseTimer()
    {
        circleFill.fillAmount = 0;
        startCountDown = false;
        timer = countDownDuration;
    }

    public void resetTimer()
    {
        circleFill.fillAmount = 1;
        startCountDown = false;
        timer = countDownDuration;
    }

    private void Start()
    {
        timer = countDownDuration;
        
        Color imageColor = centerIcon.color;
        imageColor.a = 0.5f;
        centerIcon.color = imageColor;
    }

    private void Update()
    {
        if (startCountDown)
        {
            timer -= Time.deltaTime;

            float fillAmount = Mathf.Clamp01(timer / countDownDuration);
            circleFill.fillAmount = fillAmount;

            if (timer <= 0)
            {
                circleFill.fillAmount = 1;
                StartCoroutine(ResetTimer());
            }
        }
    }
    
    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0f);
        
        timer = countDownDuration;
        startCountDown = false;
    }

    public void setIconOpaque()
    {
        Color imageColor = centerIcon.color;
        imageColor.a = 0.5f;
        centerIcon.color = imageColor;
    }

    public void removeIconOpaque()
    {
        Color imageColor = centerIcon.color;
        imageColor.a = 1f;
        centerIcon.color = imageColor;
    }
}

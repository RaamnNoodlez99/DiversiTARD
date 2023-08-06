using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Flicker : MonoBehaviour
{
    public float flickerDuration = 0.1f;
    public int flickerCount = 3;
    public SpriteRenderer flicker;

    private void Start()
    {
        flicker.enabled = false;
    }

    public void Flicker()
    {
        Debug.Log("Flicker");
        StartCoroutine(DoFlicker());
    }

    private IEnumerator DoFlicker()
    {
        for (int i = 0; i < flickerCount; i++)
        {
            yield return new WaitForSeconds(flickerDuration * 0.5f);
            flicker.enabled = true;
            yield return new WaitForSeconds(flickerDuration * 0.5f);
            flicker.enabled = false;
        }
    }
}

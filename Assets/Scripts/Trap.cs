using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool hasTrapped = false;

    public GameObject trapHolder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            if (!hasTrapped)
            {
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.cageTrap);
                hasTrapped = true;
                Destroy(trapHolder);
            }
        }
    }
}

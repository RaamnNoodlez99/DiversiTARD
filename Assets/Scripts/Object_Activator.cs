using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Activator : MonoBehaviour
{
    public GameObject objectToActivate;
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("WoodenMan") || collision.gameObject.CompareTag("Ghost"))
        {
            if (!hasActivated)
            {
                objectToActivate.SetActive(true);
                hasActivated = true;
            }
        }
    }
}

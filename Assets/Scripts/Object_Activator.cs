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

    private void Start()
    {
        GameObject ghost = GameObject.FindGameObjectWithTag("Ghost");
        GameObject woodenMan = GameObject.FindGameObjectWithTag("WoodenMan");

        Debug.Log(woodenMan.transform.position.x);

        if ( (ghost.transform.position.x > gameObject.transform.position.x || woodenMan.transform.position.x > gameObject.transform.position.x))
        {
            objectToActivate.SetActive(true);
            hasActivated = true;
        }
    }
}

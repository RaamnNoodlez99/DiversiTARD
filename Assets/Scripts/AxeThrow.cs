using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    public GameObject axe;
    public float launchForce;
    public Transform shotPoint;
    void Update()
    {
        Vector2 axePosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - axePosition;
        transform.right = direction;

        if (Input.GetKeyDown(KeyCode.L))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newAxe = Instantiate(axe, shotPoint.position, shotPoint.rotation);
        newAxe.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        

    }
}

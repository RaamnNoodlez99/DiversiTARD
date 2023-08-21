using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_Ball : MonoBehaviour
{
    public float timeToDestroy;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && !collision.gameObject.CompareTag("Projectile"))
            Destroy(gameObject, timeToDestroy);
    }
}

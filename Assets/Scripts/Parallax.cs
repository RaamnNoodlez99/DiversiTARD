using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    private Camera cam;
    public float parallaxAmount;

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxAmount;

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
    }
}

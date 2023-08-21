using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Wall : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    bool hitPlatform = false;
    bool movingUp = false;
    public bool isWallDown = false;
    string lastDirection = "up";

    Vector3 originalPosition; 
    public LayerMask platformLayerMask;

    public Transform bottomCenterPoint;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void MoveWall()
    {
        hitPlatform = false;
        if (lastDirection == "up")
        {
            MoveWallDown();
        }
        else{
            MoveWallUp();
        }
    }
    public void MoveWallDown()
    {
        isWallDown = true;
        lastDirection = "down";
        //StopCoroutine(MoveWallUpCoroutine());
        StartCoroutine(MoveWallDownCoroutine());
    }

    public void MoveWallUp()
    {
        isWallDown = false;
        lastDirection = "up";
        //StopCoroutine(MoveWallDownCoroutine());
        StartCoroutine(MoveWallUpCoroutine());
    }

    private IEnumerator MoveWallDownCoroutine()
    {
        while (!hitPlatform)
        {
            Vector3 direction = Vector3.down;

            RaycastHit2D hit = Physics2D.Raycast(bottomCenterPoint.position, direction, moveSpeed * Time.deltaTime, platformLayerMask);

            if (hit.collider != null)
            {
                hitPlatform = true;
            }
            else
            {
                Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
                transform.position = newPosition;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveWallUpCoroutine()
    {
        movingUp = true;
        while (transform.position.y < originalPosition.y)
        {
            Vector3 direction = Vector3.up;
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }
        movingUp = false;
        hitPlatform = false; 
    }
}

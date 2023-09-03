using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Wall : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    bool hitPlatform = false;
    bool movingUp = false;
    bool movingDown = false;
    public bool isWallDown = false;
    private Coroutine moveCounterReference;
    private bool hitGhostPlatform = false;
    string lastDirection = "up";

    Vector3 originalPosition; 
    public LayerMask platformLayerMask;
    public LayerMask clampLayerMask;
    public float wallStuckTime = 1f;
    public Transform bottomCenterPoint;
    public GameObject ghost;

    public Animator vineAnimation1;
    public Animator vineAnimation2;

    private bool hittingClamp = false;
    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if(ghost.GetComponent<Player_Controller>().ghostPlatformExists && hitPlatform)
        {
            Debug.Log("On Ghost Platform");
            hitGhostPlatform = true;
        }

        if (hitGhostPlatform)
        {
            if (!ghost.GetComponent<Player_Controller>().ghostPlatformExists)
            {
                Debug.Log("Out of my way");
                hitPlatform = false;
                hitGhostPlatform = false;
                MoveWallDown();
            }
        }
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
        StartCoroutine(MoveWallDownCoroutine());
    }

    public void MoveWallUp()
    {
        if (hittingClamp)
        {
            Debug.Log("set idle");
            vineAnimation1.SetBool("makeIdle", true);
            vineAnimation2.SetBool("makeIdle", true);
            hittingClamp = false;
        }
            
        lastDirection = "up";
        StartCoroutine(MoveWallUpCoroutine());
    }

    private IEnumerator MoveWallDownCoroutine()
    {
        if (moveCounterReference != null)
        {
            StopCoroutine(moveCounterReference);
        }

        movingDown = true;
        while (!hitPlatform && !movingUp)
        {
            Vector3 direction = Vector3.down;

            RaycastHit2D hit = Physics2D.Raycast(bottomCenterPoint.position, direction, moveSpeed * Time.deltaTime, platformLayerMask);
            RaycastHit2D clampHit = Physics2D.Raycast(bottomCenterPoint.position, direction, moveSpeed * Time.deltaTime, clampLayerMask);


            if (hit.collider != null)
            {
                hitPlatform = true;
                moveCounterReference = StartCoroutine(MoveCounter());
            }
            else if (clampHit.collider != null)
            {
                hitPlatform = true;
                
                //hits clamp
                vineAnimation1.SetTrigger("triggerVine");
                vineAnimation2.SetTrigger("triggerVine");
                hittingClamp = true;

                if (moveCounterReference != null)
                    StopCoroutine(moveCounterReference);
            }
            else
            {
                Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
                transform.position = newPosition;
            }

            yield return new WaitForEndOfFrame();
        }
        movingDown = false;
    }

    private IEnumerator MoveCounter()
    {
        yield return new WaitForSeconds(wallStuckTime);
        MoveWallUp();
    }

    private IEnumerator MoveWallUpCoroutine()
    {
        if (moveCounterReference != null)
        {
            StopCoroutine(moveCounterReference);
        }
        
        vineAnimation1.SetTrigger("triggerIdle");
        vineAnimation2.SetTrigger("triggerIdle");

        movingUp = true;
        while (transform.position.y < originalPosition.y && !movingDown)
        {
            Vector3 direction = Vector3.up;
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }
        movingUp = false;
        hitPlatform = false;
        isWallDown = false;
        
        vineAnimation1.SetBool("makeIdle", false);
        vineAnimation2.SetBool("makeIdle", false);
    }
}

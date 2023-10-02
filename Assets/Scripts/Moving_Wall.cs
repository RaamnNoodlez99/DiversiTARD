using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Wall : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private bool hitPlatform = false;
    bool movingUp = false;
    bool movingDown = false;
    public bool isWallDown = false;
    private Coroutine moveCounterReference;
    private bool hitGhostPlatform = false;
    private bool hasNotPlayedTrap = true;
    string lastDirection = "up";

    Vector3 originalPosition; 
    public LayerMask platformLayerMask;
    public LayerMask clampLayerMask;
    public float wallStuckTime = 1f;
    public Transform bottomCenterPoint;
    public GameObject ghost;
    public AudioSource vineWrapping;
    public bool startDownwards = false;
    public bool onlyMoveOnce = false;

    public Animator vineAnimation1;
    public Animator vineAnimation2;

    private bool hittingClamp = false;
    private void Start()
    {
        originalPosition = transform.position;

        if (startDownwards)
            StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.8f); // Adjust the delay time as needed
        MoveWall();
    }

    public void togglePlatform()
    {
        if (hitGhostPlatform)
        {
            hitPlatform = false;
            hitGhostPlatform = false;
            MoveWallDown();
        }
        else
            Invoke("checkHitStatus", 0.01f);
    }

    private void checkHitStatus()
    {
        if (hitPlatform)
        {
            hitGhostPlatform = true;
        }
    }

    private void Update()
    {
        /*if(ghost.GetComponent<Player_Controller>().ghostPlatformExists && hitPlatform)
        {
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
        }*/
        
    }

    int moveCounter = -1;

    public void MoveWall()
    {
        if (onlyMoveOnce)
        {
            if( moveCounter < 0)
            {
                hitPlatform = false;
                if (lastDirection == "up")
                {
                    MoveWallDown();
                }
                else
                {
                    MoveWallUp();
                }
            }
            moveCounter++;
        }
        else
        {
            hitPlatform = false;
            if (lastDirection == "up")
            {
                MoveWallDown();
            }
            else
            {
                MoveWallUp();
            }
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
           // Debug.Log("set idle");
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
        while (!hitPlatform && !hittingClamp && !movingUp)
        {
            Vector3 direction = Vector3.down;

            RaycastHit2D hit = Physics2D.Raycast(bottomCenterPoint.position, direction, moveSpeed * Time.deltaTime, platformLayerMask);
            RaycastHit2D clampHit = Physics2D.Raycast(bottomCenterPoint.position, direction, moveSpeed * Time.deltaTime, clampLayerMask);


            if (hit.collider != null)
            {
                Debug.Log("Hit Platform");
                hitPlatform = true;
                moveCounterReference = StartCoroutine(MoveCounter());
            }
            else if (clampHit.collider != null)
            {
                Debug.Log("Hit Vine");
                vineWrapping.Play();
                //hitPlatform = true;
                
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

    public void PlayTrapped()
    {
        if (hasNotPlayedTrap)
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.trapped);
            hasNotPlayedTrap = false;
        }
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
        
       // vineAnimation1.SetTrigger("triggerIdle");
        //vineAnimation2.SetTrigger("triggerIdle");

        movingUp = true;
       // Debug.Log("Current Position: " + transform.position.y + "    Target Position: " + originalPosition.y);
        while (transform.position.y < originalPosition.y && !movingDown)
        {
           // Debug.Log("moving");
            Vector3 direction = Vector3.up;
            Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }
        movingUp = false;
        isWallDown = false;
        hitPlatform = false;
        hitGhostPlatform = false;
        Debug.Log("Done Moving Up");
        
        vineAnimation1.SetBool("makeIdle", false);
        vineAnimation2.SetBool("makeIdle", false);
    }

    public bool getHittingClamp()
    {
        return hittingClamp;
    }
}

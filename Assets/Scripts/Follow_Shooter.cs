using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Shooter : MonoBehaviour
{
    public GameObject woodenMan;
    public GameObject ghost;
    public GameObject rail;
    public GameObject shooter;
    public GameObject stopWall;
    public AudioSource shooterMovement;
    public bool followOnlyGhost = false;
    public bool followOnlyWoodenMan = false;
    public float moveSpeed;

    Renderer railRenderer;
    private Vector3 initialShooterPosition;
    Bounds railBounds;
    GameObject followedObject;
    bool shouldSwitch = true;
    bool shooterToRight;
    private bool isResettingPosition = false;
    bool shouldPlayAudio = false;
    bool isPlayingAudio = false;

    private bool passedStage1 = false;

    public Animator shooterCircleAnimator;

    public GameObject firefliesStage1;
    public GameObject firefliesStage2;
    public GameObject movingWall;
    
    private bool shouldCheck = false;
    private float startTime;
    private bool startSecondTime = false;

    private bool shouldCheck2 = false;

    private bool doOnce = false;

    private float firstStageCompletionTime;
    private void Start()
    {
        startTime = Time.time;
        shooterMovement.loop = true;
        initialShooterPosition = shooter.transform.position;
        railRenderer = rail.GetComponent<Renderer>();
        railBounds = railRenderer.bounds;

        if (followOnlyWoodenMan)
        {
            followedObject = woodenMan;
            shouldSwitch = false;
        }
        else if (followOnlyGhost)
        {
            followedObject = ghost;
            shouldSwitch = false;
        }
        else
        {
            followedObject = woodenMan;
        }
    }

    public void ResetPosition()
    {
        if (!isResettingPosition)
        {
            isResettingPosition = true;
            //StartCoroutine(MoveToOriginalPosition());
        }
    }

    void Update()
    {

        if (Time.time - startTime >= 60f)
        {
            shouldCheck = true;
        }

        if (startSecondTime)
        {
           // Debug.Log("in second time");
           // Debug.Log("time: " + Time.time);
          //  Debug.Log("first stage" + firstStageCompletionTime);
            if (Time.time - firstStageCompletionTime >= 60f)
            {
             //   Debug.Log("lolol");
                shouldCheck2 = true;
            }
        }

        if (shouldCheck2)
        {
            if(firefliesStage2 != null)
                firefliesStage2.SetActive(true);
        }
        

        if (shouldCheck)
        {
            if (firefliesStage1 != null)
            {
                if (!passedStage1)
                {
                    firefliesStage1.SetActive(true);
                }
                else
                {
                    if (!doOnce)
                    {
                        firefliesStage1.SetActive(false);
                        startSecondTime = true;
                        firstStageCompletionTime = Time.time;
                        doOnce = true;
                    }
                }
            }
        }

        //Debug.Log(initialShooterPosition.position.x);
        if (!isResettingPosition)
        {
            if (followedObject != null)
            {
                if (shouldSwitch && !followedObject.CompareTag(followedObject.GetComponent<Character_Switch>().getCurCharacter()))
                {
                    if (followedObject == woodenMan)
                    {
                        followedObject = ghost;
                    }
                    else
                    {
                        followedObject = woodenMan;
                    }
                }
            }

            if (stopWall != null && stopWall.GetComponent<Moving_Wall>().isWallDown)
            {
                if (shooter.transform.position.x > stopWall.transform.position.x)
                {
                    shooterToRight = true;
                }
                else
                {
                    shooterToRight = false;
                }
            }

            if (followedObject)
            {
                if(shooter.transform.position.x - 2 < followedObject.transform.position.x && shooter.transform.position.x + 2 > followedObject.transform.position.x )
                {
                    //  Debug.Log("Shooter standing still");
                    shooterCircleAnimator.speed = 0f;
                
                    shouldPlayAudio = false;
                    if(!shouldPlayAudio && isPlayingAudio)
                    {
                        shooterMovement.Pause();
                        isPlayingAudio = false;
                    }
                }
                else if (shooter.transform.position.x < railBounds.max.x && shooter.transform.position.x < followedObject.transform.position.x)
                {
                    //Move right 
                    

                  //  Debug.Log("going right");

                    if (stopWall != null && stopWall.GetComponent<Moving_Wall>().isWallDown)
                    {
                        if (shooterToRight)
                        {
                            shooterCircleAnimator.speed = 1f;

                            shouldPlayAudio = true;

                            if (shouldPlayAudio && !isPlayingAudio)
                            {
                                shooterMovement.Play();
                                isPlayingAudio = true;
                            }

                            shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                        }
                        else if (!shooterToRight && shooter.transform.position.x < stopWall.transform.position.x - 5)
                        {
                            shooterCircleAnimator.speed = 1f;

                            shouldPlayAudio = true;

                            if (shouldPlayAudio && !isPlayingAudio)
                            {
                                shooterMovement.Play();
                                isPlayingAudio = true;
                            }

                            shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            shooterCircleAnimator.speed = 0f;

                            shouldPlayAudio = false;
                            if (!shouldPlayAudio && isPlayingAudio)
                            {
                                shooterMovement.Pause();
                                isPlayingAudio = false;
                            }
                        //    Debug.Log("Stopped by wall");
                            
                            if (shouldCheck)
                            {
                                if (movingWall != null)
                                {
                                    if (!movingWall.GetComponent<Moving_Wall>().getHittingClamp())
                                    {
                                        passedStage1 = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        shooterCircleAnimator.speed = 1f;

                        shouldPlayAudio = true;

                        if (shouldPlayAudio && !isPlayingAudio)
                        {
                            shooterMovement.Play();
                            isPlayingAudio = true;
                        }

                        shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                    }
                }
                else if (shooter.transform.position.x > railBounds.min.x && shooter.transform.position.x > followedObject.transform.position.x)
                {
                    //Move left
                    
                   // Debug.Log("going left");

                    if (stopWall != null && stopWall.GetComponent<Moving_Wall>().isWallDown)
                    {
                        if (shooterToRight && shooter.transform.position.x > stopWall.transform.position.x + 5)
                        {
                            shouldPlayAudio = true;

                            if (shouldPlayAudio && !isPlayingAudio)
                            {
                                shooterMovement.Play();
                                isPlayingAudio = true;
                            }
                            shooterCircleAnimator.speed = 1f;

                            shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                        }
                        else if (!shooterToRight)
                        {
                            shouldPlayAudio = true;

                            if (shouldPlayAudio && !isPlayingAudio)
                            {
                                shooterMovement.Play();
                                isPlayingAudio = true;
                            }
                            shooterCircleAnimator.speed = 1f;

                            shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            shooterCircleAnimator.speed = 0f;

                            shouldPlayAudio = false;
                            if (!shouldPlayAudio && isPlayingAudio)
                            {
                                shooterMovement.Pause();
                                isPlayingAudio = false;
                            }

                      //      Debug.Log("Stopped by wall");

                            if (shouldCheck)
                            {
                                if (movingWall != null)
                                {
                                    if (!movingWall.GetComponent<Moving_Wall>().getHittingClamp())
                                    {
                                        passedStage1 = true;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        shouldPlayAudio = true;

                        if (shouldPlayAudio && !isPlayingAudio)
                        {
                            shooterMovement.Play();
                            isPlayingAudio = true;
                        }
                        shooterCircleAnimator.speed = 1f;

                        shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                    }
                }
                else
                {
               //     Debug.Log("Rail Edge");
                    shooterCircleAnimator.speed = 0f;
                    
                    shouldPlayAudio = false;
                    if (!shouldPlayAudio && isPlayingAudio)
                    {
                        shooterMovement.Pause();
                        isPlayingAudio = false;
                    }
                }
            }

            
        }
        else
        {
            shouldPlayAudio = true;

            if (shouldPlayAudio && !isPlayingAudio)
            {
                shooterMovement.Play();
                isPlayingAudio = true;
            }

            if (initialShooterPosition.x > shooter.transform.position.x)
                shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            else
                shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);

            if(shooter.transform.position.x > initialShooterPosition.x -1 && shooter.transform.position.x < initialShooterPosition.x + 1)
            {
                isResettingPosition = false;
                shouldPlayAudio = false;
                if (!shouldPlayAudio && isPlayingAudio)
                {
                    shooterMovement.Pause();
                    isPlayingAudio = false;
                }
            }
        }
    }
}

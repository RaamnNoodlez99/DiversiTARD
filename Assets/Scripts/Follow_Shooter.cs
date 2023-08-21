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
    public bool followOnlyGhost = false;
    public bool followOnlyWoodenMan = false;
    public float moveSpeed;

    Renderer railRenderer;
    Bounds railBounds;
    GameObject followedObject;
    bool shouldSwitch = true;
    bool shooterToRight;


    private void Start()
    {
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

    void Update()
    {
        if(shouldSwitch && !followedObject.CompareTag(followedObject.GetComponent<Character_Switch>().getCurCharacter()))
        {
            if(followedObject == woodenMan)
            {
                followedObject = ghost;
            }
            else
            {
                followedObject = woodenMan;
            }
        }

        if (stopWall.GetComponent<Moving_Wall>().isWallDown)
        {
            if(shooter.transform.position.x > stopWall.transform.position.x)
            {
                shooterToRight = true;
            }
            else
            {
                shooterToRight = false;
            }
        }

        if(shooter.transform.position.x < railBounds.max.x && shooter.transform.position.x < followedObject.transform.position.x)
        {
            //Move right 

            if (stopWall.GetComponent<Moving_Wall>().isWallDown)
            {
                if (shooterToRight)
                {
                    shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                else if(!shooterToRight && shooter.transform.position.x < stopWall.transform.position.x - 1)
                {
                    shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                shooter.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
        }
        else if (shooter.transform.position.x > railBounds.min.x && shooter.transform.position.x > followedObject.transform.position.x)
        {
            //Move left

            if (stopWall.GetComponent<Moving_Wall>().isWallDown)
            {
                if (shooterToRight && shooter.transform.position.x > stopWall.transform.position.x + 1)
                {
                    shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                else if (!shooterToRight)
                {
                    shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                shooter.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
        }
    }
}

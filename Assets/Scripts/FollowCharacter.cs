using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform dadTransform;
    public float followDelay = 0.2f;
    public float followDistance = 2.0f;
    public float moveSpeed = 5.0f;

    public GameObject gameManager;

    private bool canFollow = true;
    private Game_Over gameOverScript;
    private void Start()
    {
        StartCoroutine(StartFollowing());
        gameOverScript = gameManager.GetComponent<Game_Over>();
    }
    
    public IEnumerator StartFollowing()
    {
        yield return new WaitForSeconds(followDelay);
        canFollow = true;
    }
    
    private void Update()
    {
        if (canFollow && !gameOverScript.isGameOver())
        {
            Vector3 targetPosition = dadTransform.position - (dadTransform.right * followDistance);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followDelay * Time.deltaTime * moveSpeed);
        }
    }
    
    public void StopFollowing()
    {
        canFollow = false;
    }
}

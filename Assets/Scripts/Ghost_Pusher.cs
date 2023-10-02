using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Pusher : MonoBehaviour
{
    public float movementSpeed;
    public float moveDistance;
    private bool busyPushing = false;

    public void PushGhost()
    {
        if (!busyPushing)
        {
            busyPushing = true;
            StartCoroutine(MoveObject());
        }
    }

    private IEnumerator MoveObject()
    {
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + Vector3.left * moveDistance; // Move left

        float journeyLength = Vector3.Distance(originalPosition, targetPosition);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            yield return null;
        }

        // Swap target and original positions for the return journey
        Vector3 temp = originalPosition;
        originalPosition = targetPosition;
        targetPosition = temp;

        startTime = Time.time;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float distanceCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);

            yield return null;
        }

        busyPushing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Children : MonoBehaviour
{
    public float totalTime = 5.0f; // Total time in seconds
    private float childActivationInterval; // Time interval between each child activation
    public float startTime = 2.0f; // Delay before starting the activation
    private Transform[] children;
    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        // Get all child transforms
        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            children[i].gameObject.SetActive(false);
        }

        // Calculate the time interval between each child activation
        childActivationInterval = totalTime / children.Length;

        // Start the activation process after the specified startTime
        StartCoroutine(ActivateChildrenWithDelay());
    }

    IEnumerator ActivateChildrenWithDelay()
    {
        // Wait for the specified startTime before starting the activation
        yield return new WaitForSeconds(startTime);

        // Activate children one by one
        while (currentIndex < children.Length)
        {
            timer += Time.deltaTime;

            // Check if it's time to activate the next child
            if (timer >= childActivationInterval)
            {
                children[currentIndex].gameObject.SetActive(true);
                currentIndex++;
                timer = 0f;
            }

            yield return null;
        }
    }
}

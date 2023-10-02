using UnityEngine;

public class Show_Children : MonoBehaviour
{
    public float totalTime = 5.0f; // Total time in seconds
    private float childActivationInterval; // Time interval between each child activation
    public float startTime = 2.0f; // Delay before starting the activation
    private Transform[] children;
    private int currentIndex = 0;
    private float timer = 0f;
    private bool isActivationStarted = false;

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
    }

    void FixedUpdate()
    {
        if (!isActivationStarted && Time.time >= startTime)
        {
            isActivationStarted = true;
        }

        if (isActivationStarted && currentIndex < children.Length)
        {
            timer += Time.fixedDeltaTime;

            // Check if it's time to activate the next child
            if (timer >= childActivationInterval)
            {
                children[currentIndex].gameObject.SetActive(true);
                currentIndex++;
                timer = 0f;
            }
        }
    }
}

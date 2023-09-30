using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public float startTime;
    public float endTime;
    public bool animateCameraLeft = false;
    public float cameraDistance; // The distance the camera should move
    private bool isCutsceneActive = false;
    public float cameraX;
    public float cameraY;
    public float cameraSize;

    private Camera mainCamera;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float journeyLength;
    private bool hasChangedCamPosition = false;

    void Start()
    {
        DisableAllChildRenderers();
        mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;

       
    }

    void Update()
    {
        if (Time.time >= startTime && Time.time <= endTime)
        {
            if (!isCutsceneActive)
            {
                isCutsceneActive = true;
                EnableAllChildRenderers();
            }


            if (!hasChangedCamPosition)
            {
                mainCamera.orthographicSize = cameraSize;

                Vector3 newPosition = new Vector3(cameraX, cameraY, mainCamera.transform.position.z);
                mainCamera.transform.position = newPosition;
                originalPosition = mainCamera.transform.position;

                if (animateCameraLeft)
                {
                    targetPosition = originalPosition - Vector3.right * cameraDistance;
                }
                else
                {
                    targetPosition = originalPosition + Vector3.right * cameraDistance;
                }

                journeyLength = Vector3.Distance(originalPosition, targetPosition);

                hasChangedCamPosition = true;
            }
            


            // Calculate the progress based on elapsed time within the endTime range
            float progress = (Time.time - startTime) / (endTime - startTime);
            progress = Mathf.Clamp01(progress); // Ensure progress stays within [0, 1]

            // Move the camera based on the progress
            mainCamera.transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);
        }
        else if (Time.time > endTime && isCutsceneActive)
        {
            isCutsceneActive = false;
            DisableAllChildRenderers();
            mainCamera.transform.position = originalPosition;
        }
    }

    // When end time is reached
    void DisableAllChildRenderers()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    // When start time is reached
    void EnableAllChildRenderers()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}

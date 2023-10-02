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
    public GameObject nextCutscene;
    public GameObject cutsceneManager;
    public GameObject ressurectPhases;
    public bool isFirstCutscene = false;

    private Camera mainCamera;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private float journeyLength;
    private bool hasChangedCamPosition = false;
    private float totalTime;
    private bool shouldStart = false;
    private float progress = 0f; // New progress variable
    private float currentTime = 0f; // New currentTime variable



    void Awake()
    {
        totalTime = endTime - startTime;
        DisableAllChildRenderers();
        mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;

        if (isFirstCutscene)
        {
            if (cutsceneManager != null)
                cutsceneManager.GetComponent<Cutscene_Manager>().StartCutsceneCount();

            PlayCutscene();
        }
    }

    public void PlayCutscene()
    {
        shouldStart = true;

        if (ressurectPhases != null)
            ressurectPhases.GetComponent<Show_Children>().ShowChildren();
    }

    void FixedUpdate()
    {
        if (shouldStart)
        {
            if (!isCutsceneActive)
            {
                isCutsceneActive = true;
                EnableAllChildRenderers();
                currentTime = 0f; // Reset currentTime when the cutscene starts
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

            // Calculate the current time within the cutscene
            currentTime += Time.fixedDeltaTime;

            // Calculate the progress based on currentTime and totalTime
            progress = Mathf.Clamp01(currentTime / totalTime);

            // Move the camera based on the progress
            mainCamera.transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);

            // Check if the cutscene should end
            if (currentTime >= totalTime)
            {
                isCutsceneActive = false;
                shouldStart = false;
                DisableAllChildRenderers();
                mainCamera.transform.position = originalPosition;

                if (nextCutscene != null)
                    nextCutscene.GetComponent<Cutscene>().PlayCutscene();
            }
        }
    }

    void DisableAllChildRenderers()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    void EnableAllChildRenderers()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}

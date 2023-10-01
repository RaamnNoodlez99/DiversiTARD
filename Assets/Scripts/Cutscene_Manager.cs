using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_Manager : MonoBehaviour
{
    public GameObject fadeScreen;
    public float cutSceneTotalTime;
    public float fadeOutTime;
    private SpriteRenderer fadeScreenSpriteRenderer;
    private Color originalColor;
    public AudioSource backgroundMusic;
    private float originalVolume;

    private void Start()
    {
        backgroundMusic.Play();
        fadeScreenSpriteRenderer = fadeScreen.GetComponent<SpriteRenderer>();
        originalColor = fadeScreenSpriteRenderer.color;
        originalVolume = backgroundMusic.volume;

        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / 1f;
            fadeScreenSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - elapsedTime);
            yield return null;
        }

        // Wait for the duration of the cutscene
        yield return new WaitForSeconds(cutSceneTotalTime);

        // Fade out
        float fadeOutStartTime = Time.time;
        while (Time.time < fadeOutStartTime + fadeOutTime)
        {
            // Fade out backgroundMusic
            float fadeElapsedTime = Time.time - fadeOutStartTime;
            float fadeProgress = fadeElapsedTime / fadeOutTime;
            fadeScreenSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeProgress);

            // Reduce the volume of backgroundMusic gradually
            backgroundMusic.volume = Mathf.Lerp(originalVolume, 0f, fadeProgress);

            yield return null;
        }

        // Load the next scene (assuming you want to load the next scene by index)
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Handle what happens when there are no more scenes to load
            SceneManager.LoadScene(0);
            //Debug.LogWarning("No more scenes to load.");
        }
    }
}

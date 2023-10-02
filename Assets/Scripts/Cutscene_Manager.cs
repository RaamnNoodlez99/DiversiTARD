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

    private bool isFadingIn = true;
    private float fadeInTimer = 1f;
    private float cutsceneTimer = 0f;
    private bool isFadingOut = false; // Added flag
    private float fadeOutTimer = 0f;

    private void Start()
    {
        backgroundMusic.Play();
        fadeScreenSpriteRenderer = fadeScreen.GetComponent<SpriteRenderer>();
        originalColor = fadeScreenSpriteRenderer.color;
        originalVolume = backgroundMusic.volume;

        // Set initial opacity to fully visible
        fadeScreenSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    private void FixedUpdate()
    {
        // Fade in
        if (isFadingIn)
        {
            fadeInTimer -= Time.fixedDeltaTime / 1f;
            fadeScreenSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeInTimer);

            if (fadeInTimer <= 0f)
            {
                isFadingIn = false;
            }
        }
        else
        {
            // Cutscene duration
            cutsceneTimer += Time.fixedDeltaTime;

            // Fade out (only if not already fading out)
            if (!isFadingOut && cutsceneTimer >= cutSceneTotalTime)
            {
                isFadingOut = true; // Start fading out
                fadeOutTimer = 0f;
            }

            if (isFadingOut)
            {
                fadeOutTimer += Time.fixedDeltaTime;
                float fadeProgress = Mathf.Clamp01(fadeOutTimer / fadeOutTime);
                fadeScreenSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f + fadeProgress);

                // Reduce the volume of backgroundMusic gradually
                float volumeChanger = 1f - fadeProgress;
                backgroundMusic.volume = Mathf.Lerp(originalVolume, 0f, volumeChanger);

                // Load the next scene when fade out is complete
                if (fadeProgress >= 1f)
                {
                    LoadNextScene();
                }
            }
        }
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
            SceneManager.LoadScene(0);
        }
    }
}

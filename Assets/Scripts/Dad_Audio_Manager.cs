using System.Collections;
using UnityEngine;

public class Dad_Audio_Manager : MonoBehaviour
{
    public AudioClip plantsRustleClip;
    private AudioSource plantsRustleAudioSource;
    public  float minTimeBetweenRustles = 10f;
    public float maxTimeBetweenRustles = 30f;

    private void Start()
    {
        plantsRustleAudioSource = gameObject.AddComponent<AudioSource>();
        plantsRustleAudioSource.clip = plantsRustleClip;
        plantsRustleAudioSource.playOnAwake = false;

        StartCoroutine(RandomlyPlayRustles());
    }

    private IEnumerator RandomlyPlayRustles()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenRustles, maxTimeBetweenRustles);
            yield return new WaitForSeconds(waitTime);

            plantsRustleAudioSource.Play();
            yield return new WaitForSeconds(plantsRustleAudioSource.clip.length);
        }
    }
}

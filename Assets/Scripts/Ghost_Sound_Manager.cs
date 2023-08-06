using System.Collections;
using UnityEngine;

public class Ghost_Sound_Manager : MonoBehaviour
{
    public AudioClip cricketClip;
    private AudioSource cricketAudioSource;
    public float minTimeBetweenChirps = 5f;
    public float maxTimeBetweenChirps = 20f;

    private void Start()
    {
        cricketAudioSource = gameObject.AddComponent<AudioSource>();
        cricketAudioSource.clip = cricketClip;
        cricketAudioSource.playOnAwake = false;

        StartCoroutine(RandomlyPlayCricketChirps());
    }

    private IEnumerator RandomlyPlayCricketChirps()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenChirps, maxTimeBetweenChirps);
            yield return new WaitForSeconds(waitTime);

            cricketAudioSource.Play();
            yield return new WaitForSeconds(cricketAudioSource.clip.length);
        }
    }
}

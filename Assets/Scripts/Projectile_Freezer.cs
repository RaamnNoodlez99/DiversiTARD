using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Freezer : MonoBehaviour
{
    public float timeToDestroy;
    public AudioSource soundSource;
    public AudioClip[] stoneFallSounds;
    public AudioClip[] arrowFallSounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("StoneBall"))
        {
            //Debug.Log("Freezing StoneBall");
            PlayRandomStoneAudio();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Arrows"))
        {
            //Debug.Log("Freezing Arrows");
            PlayRandomArrowAudio();
            Destroy(collision.gameObject);
        }
    }

    public void PlayRandomStoneAudio()
    {
        AudioClip randomSound = stoneFallSounds[Random.Range(0, stoneFallSounds.Length)];
        soundSource.clip = randomSound;
        soundSource.Play();
    }

    public void PlayRandomArrowAudio()
    {
        AudioClip randomSound = arrowFallSounds[Random.Range(0, arrowFallSounds.Length)];
        soundSource.clip = randomSound;
        soundSource.Play();
    }
}

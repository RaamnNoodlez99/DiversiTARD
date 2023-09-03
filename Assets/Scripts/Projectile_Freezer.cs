using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Freezer : MonoBehaviour
{

    public float timeToDestroy;
    public AudioSource stoneSource;
    public AudioClip[] stoneFallSounds;

    // Keep track of the last played audio clip.
    private AudioClip lastPlayedSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StoneBall"))
        {
            Debug.Log("Freezing");
            playRandomAudio();
            Destroy(collision.gameObject);
        }
    }

    public float playRandomAudio()
    {
        AudioClip randomSound;
        do
        {
            randomSound = stoneFallSounds[Random.Range(0, stoneFallSounds.Length)];
        } while (randomSound == lastPlayedSound);

        stoneSource.clip = randomSound;
        stoneSource.Play();
        lastPlayedSound = randomSound;

        return randomSound.length;
    }
}

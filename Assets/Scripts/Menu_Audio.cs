using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Audio : MonoBehaviour
{

    public AudioSource menuAudio;

    public AudioClip buttonMove;
    public AudioClip buttonPopup;

    public static Menu_Audio menuAudioInstance;


    void Awake()
    {
        if (menuAudioInstance != null && menuAudioInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        menuAudioInstance = this;
    }

    void Update()
    {
        
    }
}

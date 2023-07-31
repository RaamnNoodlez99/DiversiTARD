using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
   
    public AudioClip platfromCreation;
    public AudioClip platformDestroy;
    public AudioClip axeSwing;
    public AudioClip axeSwingWithHit;
    public AudioClip environmentChange;
    public AudioClip tutorialBackgroundMusic;

    public AudioSource Audio;
    public AudioSource BackgroundAudio;

    public static SFX_Manager sfxInstance;


    private void Awake()
    {
        if(sfxInstance != null && sfxInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
        BackgroundAudio.loop = true;
        BackgroundAudio.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

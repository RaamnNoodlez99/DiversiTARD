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
    public AudioClip poisonDrop;
    public AudioClip deathGrunt;
    public AudioClip painGrunt;
    public AudioClip footstep;
    public AudioClip batBackground;


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
        sfxInstance.BackgroundAudio.loop = true;
        DontDestroyOnLoad(this);

    }
}

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
    public AudioClip click;
    public AudioClip stoneShort;
    public AudioClip stoneSlide;
    public AudioClip jumpGrunt;
    public AudioClip jumpLand;



    public AudioSource Audio;
    public AudioSource BackgroundAudio;



    public static SFX_Manager sfxInstance;


    private void Awake()
    {
       // fatherBackground.Add(WindBackground);
        //fatherBackground.Add(BirdBackground);

       // ghostBackground.Add(BatBackGround);
        //ghostBackground.Add(CaveBackground);

        //bothBackground.Add(MusicBackground);

        //allBackground.Add(MusicBackground);
        //allBackground.Add(CaveBackground);
        //allBackground.Add(BatBackGround);
        //allBackground.Add(BirdBackground);
        //allBackground.Add(WindBackground);

        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);

    }
}

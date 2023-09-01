using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings_Manager : MonoBehaviour
{
    public static Settings_Manager instance;

    public AudioMixer audioMixer;

    const string MASTERVOL_KEY = "masterVolume";
    const string SFXVOL_KEY = "soundEffectsVolume";
    const string BACKGROUNDVOL_KEY = "backgroundVolume";
    const string TOGGLESWITCH_KEY = "toggleSwitch";


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    public void LoadVolume()
    {
        float masterVol = PlayerPrefs.GetFloat(MASTERVOL_KEY, 0.8f);
        Debug.Log(masterVol);
        float backgroundVol = PlayerPrefs.GetFloat(BACKGROUNDVOL_KEY, 0.8f);
        Debug.Log(backgroundVol);
        float sfxVol = PlayerPrefs.GetFloat(SFXVOL_KEY, 0.8f);
        Debug.Log(sfxVol);

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVol) * 20);
        audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(backgroundVol) * 20);
        audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sfxVol) * 20);
    }
}

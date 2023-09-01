using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Setting_Changer : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterVolSlider;
    public Slider backgroundVolSlider;
    public Slider sfxVolSlider;
    public Toggle toggleSwitch;


    const string MASTERVOL_KEY = "masterVolume";
    const string SFXVOL_KEY = "soundEffectsVolume";
    const string BACKGROUNDVOL_KEY = "backgroundVolume";
    const string TOGGLESWITCH_KEY = "toggleSwitch";


    void OnEnable()
    {
        LoadSettings();
    }

    public void SetMasterVolume(float sliderValue)
    {
        if(!Pause_Menu.isPaused)
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);

        PlayerPrefs.SetFloat(MASTERVOL_KEY, sliderValue);
    }

    public void SetBackgroundVolume(float sliderValue)
    {
        if (!Pause_Menu.isPaused)
            audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(sliderValue) * 20);

        PlayerPrefs.SetFloat(BACKGROUNDVOL_KEY, sliderValue);

    }

    public void SetSoundEffectsVolume(float sliderValue)
    {
        if (!Pause_Menu.isPaused)
            audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);

        PlayerPrefs.SetFloat(SFXVOL_KEY, sliderValue);
    }

    void LoadSettings()
    {
        float masterVol = PlayerPrefs.GetFloat(MASTERVOL_KEY, 0.8f);
        //Debug.Log(masterVol);
        float backgroundVol = PlayerPrefs.GetFloat(BACKGROUNDVOL_KEY, 0.8f);
        //Debug.Log(backgroundVol);
        float sfxVol = PlayerPrefs.GetFloat(SFXVOL_KEY, 0.8f);
        //Debug.Log(sfxVol);
        int toggleSwitchVal = PlayerPrefs.GetInt(TOGGLESWITCH_KEY, 1);

        masterVolSlider.value = masterVol;
        backgroundVolSlider.value = backgroundVol;
        sfxVolSlider.value = sfxVol;

        if(toggleSwitchVal == 1)
        {
            toggleSwitch.SetIsOnWithoutNotify(true);
        }
        else
        {
            toggleSwitch.SetIsOnWithoutNotify(false);
        }
    }

    public void ChangeSwitchControl()
    {
        if (PlayerPrefs.GetInt(TOGGLESWITCH_KEY) == 0)
        {
            PlayerPrefs.SetInt(TOGGLESWITCH_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(TOGGLESWITCH_KEY, 0);
        }
    }
}

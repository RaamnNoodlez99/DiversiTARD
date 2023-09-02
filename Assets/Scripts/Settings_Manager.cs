using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings_Manager : MonoBehaviour
{
    public static Settings_Manager instance;

    public AudioMixer audioMixer;

    const string MASTERVOL_KEY = "masterVolume";
    const string SFXVOL_KEY = "soundEffectsVolume";
    const string BACKGROUNDVOL_KEY = "backgroundVolume";
    const string TOGGLESWITCH_KEY = "toggleSwitch";

    void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().buildIndex);

        if (SceneManager.GetActiveScene().buildIndex != 0 && PlayerPrefs.GetInt("currentLevel") != SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("currentLevel", SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Invoke("LoadVolume", 0.01f);
    }

    public void LoadVolume()
    {
        float masterVol = PlayerPrefs.GetFloat(MASTERVOL_KEY, 1f);
        Debug.Log(masterVol);
        float backgroundVol = PlayerPrefs.GetFloat(BACKGROUNDVOL_KEY, 1f);
        Debug.Log(backgroundVol);
        float sfxVol = PlayerPrefs.GetFloat(SFXVOL_KEY, 1f);
        Debug.Log(sfxVol);

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVol) * 20);
        audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(backgroundVol) * 20);
        audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sfxVol) * 20);
    }

}

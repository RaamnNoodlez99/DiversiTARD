using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic_Handler : MonoBehaviour
{
    public GameObject characterCheck;
    public AudioSource[] audioToPauseInDadWorld;
    public AudioSource[] audioToPauseInGhostWorld;

    private string previousCharacter; 

    void Start()
    {
        previousCharacter = characterCheck.GetComponent<Character_Switch>().getCurCharacter();

        if (previousCharacter == "WoodenMan")
        {
            foreach (AudioSource source in audioToPauseInDadWorld)
            {
                source.Pause();
            }

            foreach (AudioSource source in audioToPauseInGhostWorld)
            {
                source.Play();
            }
        }
        else
        {
            foreach (AudioSource source in audioToPauseInDadWorld)
            {
                source.Play();
            }

            foreach (AudioSource source in audioToPauseInGhostWorld)
            {
                source.Pause();
            }
        }
    }

    void Update()
    {
        string currentCharacter = characterCheck.GetComponent<Character_Switch>().getCurCharacter();

        if (currentCharacter != previousCharacter)
        {
            CharacterChanged(currentCharacter);

            previousCharacter = currentCharacter;
        }
    }

    void CharacterChanged(string newCharacter)
    {
        if(newCharacter == "WoodenMan")
        {
            foreach (AudioSource source in audioToPauseInDadWorld)
            {
                source.Pause();
            }

            foreach (AudioSource source in audioToPauseInGhostWorld)
            {
                source.Play();
            }
        }
        else
        {
            foreach (AudioSource source in audioToPauseInDadWorld)
            {
                source.Play();
            }

            foreach (AudioSource source in audioToPauseInGhostWorld)
            {
                source.Pause();
            }
        }
    }
}

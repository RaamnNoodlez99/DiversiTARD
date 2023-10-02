using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight_Checker : MonoBehaviour
{
    public GameObject levelOver;
    public GameObject boss;
    public AudioSource bossdeath;

    private bool hasCalledLevelOver;

    private void Update()
    {
        if(!hasCalledLevelOver && boss == null)
        {
            bossdeath.Play();
            hasCalledLevelOver = true;
            Invoke("callLevelOver", 2f);
        }
    }

    private void callLevelOver()
    {
        levelOver.GetComponent<Level_Complete>().LevelOver();
    }
}

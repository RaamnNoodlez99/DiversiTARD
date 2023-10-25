using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase1 : MonoBehaviour
{
    //public float cameraSpeed = 7.0f;
    public float monsterSpeed = 7.0f;
   // public Transform camera;
    public Transform flowerBoss;
    public Transform followObject;


    void Update()
    {
       // camera.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        flowerBoss.Translate(Vector3.right * monsterSpeed * Time.deltaTime);
        //followObject.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open : MonoBehaviour
{
    public GameObject[] linkedTorches;
    public GameObject doorSprite;
    public float openSpeed = 10f;
    public float doorLength = 18f;
    
    private Vector3 initialPosition;
    private Vector3 openPosition;
    private float targetY;
    void Start()
    {
        initialPosition = transform.position;
        openPosition = new Vector3(initialPosition.x, initialPosition.y - doorLength, initialPosition.z);
        targetY = initialPosition.y;
    }
    
    void Update()
    {
        calculateTargetPosition();
        doorSprite.transform.position = Vector3.MoveTowards(doorSprite.transform.position,
            new Vector3(initialPosition.x, targetY, initialPosition.z), openSpeed * Time.deltaTime);
    }

    private void calculateTargetPosition()
    {
        int litCount = 0;
        for (int i = 0; i < linkedTorches.Length; i++)
        {
            if (linkedTorches[i].transform.childCount > 0 && linkedTorches[i].transform.GetChild(0).gameObject.activeSelf)
            {
                litCount++;
            }
        }

        float litPercentage = (float)litCount / linkedTorches.Length;
        targetY = Mathf.Lerp(initialPosition.y, openPosition.y, litPercentage);
    }
    
}

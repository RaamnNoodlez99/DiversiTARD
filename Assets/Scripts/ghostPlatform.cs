using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostPlatform : MonoBehaviour
{

    public float despawnDelay = 8f;
    float despawnTimer = 0f;
    bool ghostOffPlatform = false;
    public bool platformTimerResests = true;
    public GameObject ghost;
    
    private GameObject GhostHUD;


    // Start is called before the first frame update
    
    void Start()
    {
        ghost = GameObject.Find("Ghost");
        

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Ghost_HUD")
            {
                GhostHUD = obj;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Despawn();
    }

    public void SetDespawnTimer()
    {
        despawnTimer = despawnDelay - 0.7f; 
    }

    public void Despawn()
    {
        if (ghostOffPlatform)
        {
            despawnTimer += Time.deltaTime;
        }

        if(despawnTimer >= despawnDelay)
        {
            if(gameObject != null)
            {
                ghost.GetComponent<Player_Controller>().ghostPlatformExists = false;
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.platformDestroy);
                Destroy(gameObject);
                
                if (GhostHUD != null)
                {
                    Ghost_Platform_HUD ghostHud = GhostHUD.GetComponent<Ghost_Platform_HUD>();
                    ghostHud.resetTimer();
                }
            }  
        }else if (despawnDelay - despawnTimer <= 0.7f)
        {
            Animator platformAnimator = GetComponent<Animator>();
            platformAnimator.SetBool("isBoneActive", false);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("Ghost jumping");
            if (GhostHUD != null)
            {
                Ghost_Platform_HUD ghostHud = GhostHUD.GetComponent<Ghost_Platform_HUD>();
                ghostHud.pauseTimer();
            }
        }
        
        if (collision.gameObject.CompareTag("Ghost") && despawnDelay - despawnTimer >= 0.5f)
        {
            //Debug.Log("Ghost on Platform");
            ghostOffPlatform = false;
            if(platformTimerResests)
                despawnTimer = 0f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            //Debug.Log("Ghost off Platform");
            ghostOffPlatform = true;
            
            if (GhostHUD != null)
            {
                Ghost_Platform_HUD ghostHud = GhostHUD.GetComponent<Ghost_Platform_HUD>();
                ghostHud.doCountDown();
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Ghost") && GhostHUD != null)
    //     {
    //         Ghost_Platform_HUD ghostHud = GhostHUD.GetComponent<Ghost_Platform_HUD>();
    //         ghostHud.pauseTimer();
    //     }
    // }
}

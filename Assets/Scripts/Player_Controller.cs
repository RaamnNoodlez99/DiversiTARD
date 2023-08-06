using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float jumpForce = 18f;
    public float gravityScale = 25f;
    public float jumpTimer = 0.4f;

    public float platformOffset = 1f;
    public float ghostPlatSpawnDelay = 0f;
    public bool ghostPlatformExists = false;
    public float switchDelay = 0.001f;
    public GameObject inputManager;
    public GameObject ghostPlatform;
    //public GameObject gameManager;

    bool isJumping = false;
    bool activateJump = false;
    bool startTimer = false;
    bool earlyRelease = false;
    float timer;
    public static bool canSwitch = true;


    Vector2 moveInput;
    RaycastHit2D[] m_Contacts = new RaycastHit2D[100];

    public bool IsMoving { get; private set; }

    Rigidbody2D playerBody;
    public Animator animator;

    public float knockbackForce = 12;
    private float footstepTimer = 0f;
    public float footstepInterval = 0.3f;
    public float knockbackCounter;
    public float knockbackTotalTime = 0.4f;
    public bool knockFromRight;

    private bool facingLeft = true;

    private GameObject currentGhostPlatform = null;
    
    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        timer = jumpTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                earlyRelease = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (knockbackCounter <= 0)
        {
            //inputManager.GetComponent<PlayerInput>().enabled = true;
            playerBody.velocity = new Vector2(moveInput.x * walkSpeed, playerBody.velocity.y);
        }
        else
        {
            StartCoroutine(DisableInputForDuration(knockbackTotalTime + 0.2f));
            //inputManager.GetComponent<PlayerInput>().enabled = false;
            //moveInput = Vector2.zero;
            if (knockFromRight == true)
            {
                moveInput = Vector2.zero;
                playerBody.velocity = new Vector2(-knockbackForce + moveInput.x * walkSpeed, knockbackForce);
            }
            else
            {
                playerBody.velocity = new Vector2(knockbackForce + moveInput.x * walkSpeed, knockbackForce);
            }
            knockbackCounter -= Time.deltaTime;
        }

        if (IsMoving && !isJumping && footstepTimer <= 0f && gameObject.CompareTag("WoodenMan"))
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.footstep);
            footstepTimer = footstepInterval;
        }
        if (footstepTimer > 0f)
        {
            footstepTimer -= Time.fixedDeltaTime;
        }

        if (CompareTag("Ghost") || CompareTag("WoodenMan"))
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        }
        

        if(moveInput.x > 0 && facingLeft)
        {
            Flip();
            if(gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                Vector3 newPosition = transform.position + Vector3.right * 3;
                transform.position = newPosition;
            }
        }

        if(moveInput.x < 0 && !facingLeft)
        {
            Flip();
            if(gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                Vector3 newPosition = transform.position - Vector3.right * 3;
                transform.position = newPosition;
            }
        }

        if (activateJump)
        {
           playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);

            if (earlyRelease)
            {
                playerBody.gravityScale = gravityScale;
                timer = jumpTimer;
                startTimer = false;
                earlyRelease = false;
                activateJump = false;
            }
        }
           
    }

    IEnumerator DisableInputForDuration(float duration)
    {
        inputManager.GetComponent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(duration);
        inputManager.GetComponent<PlayerInput>().enabled = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(gameObject.GetComponent<Character_Switch>().getCurCharacter());
        if (gameObject.CompareTag(gameObject.GetComponent<Character_Switch>().getCurCharacter()))
        {
            moveInput = context.ReadValue<Vector2>();
            IsMoving = moveInput != Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping && gameObject.CompareTag(gameObject.GetComponent<Character_Switch>().getCurCharacter()) && !Pause_Menu.isPaused)
        {
            animator.SetTrigger("takeOff");
            activateJump = true;
            startTimer = true;
            playerBody.gravityScale = 0;
        }

        if (context.canceled || earlyRelease)
        {
            playerBody.gravityScale = gravityScale;
            timer = jumpTimer;
            startTimer = false;
            earlyRelease = false;
            activateJump = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan" && !Pause_Menu.isPaused)
            gameObject.GetComponent<Wooden_Man_Attack>().Attack();
    }

    public void OnSpawnPlatform(InputAction.CallbackContext context)
    {
        if (currentGhostPlatform != null) 
        {
            Debug.Log("But I still exist");
        }
        Debug.Log(gameObject.GetComponent<Character_Switch>().getCurCharacter());
        if (context.performed && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "Ghost" && !Pause_Menu.isPaused)
            SpawnPlatform();
    }

    public void OnSwitchCharacter(InputAction.CallbackContext context)
    {
        if (context.performed && canSwitch && !Pause_Menu.isPaused)
        {
            gameObject.GetComponent<Character_Switch>().SwitchCharacter();
            canSwitch = false;
            StartCoroutine(EnableSwitchAfterDelay());
        }
    }


    private IEnumerator EnableSwitchAfterDelay()
    {
        yield return new WaitForSeconds(switchDelay);
        canSwitch = true;
    }
    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
    //     {
    //         isJumping = false;
    //
    //         if (CompareTag("Ghost") || CompareTag("WoodenMan"))
    //             animator.SetBool("isJumping", false);
    //     }
    // }

    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
    //     {
    //         isJumping = true;
    //
    //         if (CompareTag("Ghost") || CompareTag("WoodenMan"))
    //             animator.SetTrigger("takeOff");
    //             animator.SetBool("isJumping", true);
    //     }
    // }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }

    public bool CanSpawnGhostPlatform()
    {
        if (isJumping && !ghostPlatformExists)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, platformOffset);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Platform"))
                    return false;
            }
            return true;
        }
        return false;
    }

    public void SpawnPlatform()
    {
        if (CanSpawnGhostPlatform() && CompareTag("Ghost"))
        {
            Invoke("SpawnDelayedPlatform", ghostPlatSpawnDelay);
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.platfromCreation);
            ghostPlatformExists = true;
        }
    }

    private void SpawnDelayedPlatform()
    {
        Vector3 spawnPosition = playerBody.position - new Vector2(0f, platformOffset);
        currentGhostPlatform = Instantiate(ghostPlatform, spawnPosition, Quaternion.identity);
        Animator ghostPlatformAnimator = currentGhostPlatform.GetComponent<Animator>();
        ghostPlatformAnimator.SetBool("isBoneActive", true);
    }

    public void setIsJumping(bool value)
    {
        isJumping = value;
        if (CompareTag("Ghost") || CompareTag("WoodenMan"))
        {
            animator.SetBool("isJumping", value);
        }
    }
}

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
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    public float platformOffset = 4f;
    public float ghostPlatSpawnDelay = 0.015f;
    public bool ghostPlatformExists = false;
    public float switchDelay = 0.001f;
    public GameObject inputManager;
    public GameObject ghostPlatform;
    public bool poisonKnockback = true;
    //public GameObject gameManager;

    bool isJumping = false;
    bool isAttacking = false;
    bool activateJump = false;
    bool startTimer = false;
    bool earlyRelease = false;
    bool despawnAvaialable = false;
    static bool switchToGhost = false;
    static bool switchToFather = false;
    float timer;
    public static bool canSwitch = true;
    
    public GameObject GhostHUD;


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
    public bool IsBusyTeleporting = false;
    public GameObject movingWall;


    private bool facingLeft = true;
    private bool movementFrozen = false;

    private GameObject currentGhostPlatform = null;

    private GameObject ghostObject;
    public bool ghostCanFollow;
    
    
    
    private Animator ghostObjectAnimator;
    private Animator woodenManAnimator;
    private GameObject dadObject;
    private bool doOnce = false;
    
    bool isFlickering = false;
    bool didDisappear = false;

    private void Awake()
    {
        if (ghostCanFollow)
        {
            ghostObject = GameObject.FindWithTag("Ghost");
            dadObject = GameObject.FindWithTag("WoodenMan");
            ghostObjectAnimator = ghostObject.GetComponent<Animator>();
            woodenManAnimator = dadObject.GetComponent<Animator>();
        }
        playerBody = GetComponent<Rigidbody2D>();
        timer = jumpTimer;
        playerBody.gravityScale = gravityScale;
    }
    
    bool isGrounded()
    {
        int groundLayerMask = LayerMask.GetMask("Platform", "FallingPlatform");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, groundLayerMask);

        Debug.DrawRay(transform.position, Vector2.down * 5f, Color.yellow, 0.1f);
        
        return hit.collider != null;
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

        if (currentGhostPlatform == null)
        {
            despawnAvaialable = false;
        }

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
        {
            if (ghostCanFollow)
            {
                float woodenManMoveSpeed = this.woodenManAnimator.GetFloat("Speed");

                if (woodenManMoveSpeed > 0f)
                {
                    ghostObjectAnimator.SetFloat("Speed", 0.1f);

                    if (!doOnce)
                    {
                        ghostObject.GetComponent<Player_Controller>().Flip();
                        doOnce = true;
                    }
                }
                else
                {
                    ghostObjectAnimator.SetFloat("Speed", 0f);

                    if (doOnce)
                    {
                        ghostObject.GetComponent<Player_Controller>().Flip();
                        doOnce = false;
                    }
                }
                
                float distance = Vector3.Distance(transform.position, ghostObject.transform.position);
                if (distance < 4f)
                {
                    this.EnableRenderers(ghostObject.transform);
                    
                    ghostObject.GetComponent<FollowCharacter>().enabled = false;
                }
                else
                {
                    if (distance > 20f)
                    {
                        // Make the ghost object invisible
                        this.DisableRenderers(ghostObject.transform);
                        this.didDisappear = true;
                    }
                    
                    if (distance <= 5f)
                    {
                        if (!isFlickering && didDisappear)
                        {
                            StartCoroutine(Flicker());
                        }
                    }
                    
                    ghostObject.GetComponent<FollowCharacter>().enabled = true;
                }
            }
            
            Animator woodenManAnimator = gameObject.GetComponent<Animator>();
            if (woodenManAnimator.GetBool("isAttacking") == false)
            {
                isAttacking = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                movementFrozen = false;
            }
            else
            {
                inputManager.SetActive(false);
                Invoke("resetInputManager", 1f);
                movementFrozen = true;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        if (!Pause_Menu.isPaused)
        {
            if(switchToGhost && gameObject.CompareTag("WoodenMan") && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                gameObject.GetComponent<Character_Switch>().SwitchCharacter();
            }
            else if (switchToFather && gameObject.CompareTag("Ghost") && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "Ghost")
            {
                gameObject.GetComponent<Character_Switch>().SwitchCharacter();
            }
            switchToGhost = false;
            switchToFather = false;
        }

        if (CompareTag("Ghost"))
        {
            if (gameObject.GetComponent<Character_Switch>().getCurCharacter() == "Ghost")
            {
                if(ghostCanFollow)
                    gameObject.GetComponent<FollowCharacter>().enabled = false;
                if (GhostHUD != null)
                {
                    Ghost_Platform_HUD ghostHud = GhostHUD.GetComponent<Ghost_Platform_HUD>();

                    /* if (isJumping && !ghostPlatformExists)
                     {
                         ghostHud.removeIconOpaque();
                     }
                     else if(!isJumping && ghostPlatformExists && despawnAvaialable)
                     {
                         ghostHud.removeIconOpaque();
                     }
                     else if (!isJumping && !despawnAvaialable && !ghostPlatformExists)
                     {
                         ghostHud.setIconOpaque();
                     }
                     else if(!despawnAvaialable)
                     {
                         ghostHud.setIconOpaque();
                     }*/
                    if (isJumping)
                    {
                        ghostHud.removeIconOpaque();
                    }
                    else if(!isJumping && currentGhostPlatform == null)
                    {
                        ghostHud.setIconOpaque();
                    }
                    else if (!isJumping && currentGhostPlatform != null)
                    {
                        ghostHud.removeIconOpaque();
                    }
                }
            }
        }
    }

    private void resetInputManager()
    {
        inputManager.SetActive(true);
    }
    
    IEnumerator Flicker()
    {
        isFlickering = true;

        for (int i = 0; i < 5; i++)
        {
            EnableRenderers(ghostObject.transform);
            yield return new WaitForSeconds(0.1f);
            DisableRenderers(ghostObject.transform);
            yield return new WaitForSeconds(0.1f);
        }

        EnableRenderers(ghostObject.transform);
        isFlickering = false;
        didDisappear = false;
    }

    private void FixedUpdate()
    {
        if (knockbackCounter <= 0)
        {
            playerBody.velocity = new Vector2(moveInput.x * walkSpeed, playerBody.velocity.y);
        }
        else
        {
            if (poisonKnockback)
            {
                StartCoroutine(DisableInputForDuration(knockbackTotalTime + 0.4f));
                if (knockFromRight == true)
                {
                    moveInput = Vector2.zero;
                    playerBody.velocity = new Vector2(knockbackForce + moveInput.x * walkSpeed, knockbackForce);
                }
                else
                {
                    playerBody.velocity = new Vector2(-knockbackForce + moveInput.x * walkSpeed, knockbackForce);
                }
                knockbackCounter -= Time.deltaTime;
            }
            else
            {
                StartCoroutine(DisableInputForDuration(knockbackTotalTime + 0.4f));
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
        }

        if (IsMoving && !isJumping && footstepTimer <= 0f && gameObject.CompareTag("WoodenMan") && !IsBusyTeleporting)
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.footstep,0.6f);
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
        

        if(!isAttacking && moveInput.x > 0 && facingLeft)
        {
            Flip();
            if(gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                Vector3 newPosition = transform.position + Vector3.right * 3;
                transform.position = newPosition;
            }
        }

        if(!isAttacking && moveInput.x < 0 && !facingLeft)
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
            coyoteTimeCounter = 0;
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

    public IEnumerator FreezeMovementInputForDuration(float duration)
    {
        moveInput = Vector2.zero;
        Debug.Log("Freesing movement for: " + gameObject.tag);
        movementFrozen = true;
        yield return new WaitForSeconds(duration);
        movementFrozen = false;
    }

    public void FreezeMovementIndefinetely()
    {
        moveInput = Vector2.zero;
        movementFrozen = true;
    }

    IEnumerator DisableInputForDuration(float duration)
    {
        inputManager.GetComponent<PlayerInput>().enabled = false;
        yield return new WaitForSeconds(duration);
        inputManager.GetComponent<PlayerInput>().enabled = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!movementFrozen)
        {
            IsBusyTeleporting = false;
            if (gameObject.CompareTag(gameObject.GetComponent<Character_Switch>().getCurCharacter()) && knockbackCounter <= 0)
            {
                moveInput = context.ReadValue<Vector2>();
                IsMoving = moveInput != Vector2.zero;
            }
            else
            {
                moveInput = Vector2.zero;
                IsMoving = false;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isAttacking && !movementFrozen)
        {
            if (context.performed && coyoteTimeCounter > 0 && !Pause_Menu.isPaused && !Level_Complete.levelIsOver)
            {
                if(gameObject.CompareTag("WoodenMan") && GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
                    SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.jumpGrunt);
    
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
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!isJumping && !isAttacking)
        {
            if (context.performed && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan" &&
                !Pause_Menu.isPaused)
            {
                // gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                // gameObject.GetComponent<Wooden_Man_Attack>().Attack();
                // isAttacking = true;
                gameObject.GetComponent<Wooden_Man_Attack>().Attack();
                isAttacking = true;
            }
        }
    }

public void OnSpawnPlatform(InputAction.CallbackContext context)
    { 
        if(movingWall != null)
        {
            movingWall.GetComponent<Moving_Wall>().togglePlatform();
        }

        if(gameObject.GetComponent<Character_Switch>().getCurCharacter() == "Ghost" && gameObject.CompareTag("Ghost") && !Pause_Menu.isPaused && isJumping && context.performed)
        {
            Debug.Log("1");
            if (currentGhostPlatform != null)
            {
                Debug.Log("Destroying Old Creating New");
                currentGhostPlatform.GetComponent<ghostPlatform>().SetDespawnTimer();
                ghostPlatformExists = false;
                SpawnPlatform();
            }
            else
            {
                Debug.Log("Creating New");
                SpawnPlatform();
            }
        }
        else if (gameObject.GetComponent<Character_Switch>().getCurCharacter() == "Ghost" && gameObject.CompareTag("Ghost") && !Pause_Menu.isPaused && !isJumping && context.performed)
        {
            Debug.Log("2");
            if (currentGhostPlatform != null)
            {
                Debug.Log("Destroying Old");
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.platformDestroy,0.55f);
                currentGhostPlatform.GetComponent<ghostPlatform>().SetDespawnTimer();
                ghostPlatformExists = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentGhostPlatform != null && collision.gameObject.CompareTag("Platform") && collision.gameObject != currentGhostPlatform && gameObject.CompareTag("Ghost"))
        {
            despawnAvaialable = true;
        } else if (currentGhostPlatform != null && collision.gameObject == currentGhostPlatform && gameObject.CompareTag("Ghost"))
        {
            despawnAvaialable = false;
        }
    }

    public void OnSwitchCharacter(InputAction.CallbackContext context)
    {
        if (PlayerPrefs.GetInt("toggleSwitch") == 1)
        {
            if (context.performed)
            {
                if (gameObject.CompareTag("WoodenMan") && !Pause_Menu.isPaused)
                {
                    GameObject currentCharacter = GameObject.FindWithTag(gameObject.GetComponent<Character_Switch>().getCurCharacter());
                    currentCharacter.GetComponent<Character_Switch>().SwitchCharacter();
                }
            }
        }
        else
        {
            if (gameObject.CompareTag("WoodenMan") && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                if (context.performed)
                {
                    if (!Pause_Menu.isPaused)
                    {
                        gameObject.GetComponent<Character_Switch>().SwitchCharacter();
                    }
                }
            }
            else if (gameObject.CompareTag("Ghost") && gameObject.GetComponent<Character_Switch>().getCurCharacter() == "Ghost")
            {
                if (context.canceled)
                {
                    if (!Pause_Menu.isPaused)
                    {
                        gameObject.GetComponent<Character_Switch>().SwitchCharacter();
                    }
                }
            }
        }
    
        if (Pause_Menu.isPaused && PlayerPrefs.GetInt("toggleSwitch") == 0)
        {
            if(context.performed)
            {
                switchToGhost = true;
                switchToFather = false;
            }
            if (context.canceled)
            {
                switchToGhost = false;
                switchToFather = true;
            }
        }
    }

    

    public void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }

    public bool CanSpawnGhostPlatform()
    {
        if (isJumping)
        {
           /* Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, platformOffset);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Platform"))
                    return false;
            }*/
            return true;
        }
        return false;
    }

    public void SpawnPlatform()
    {
        if (CanSpawnGhostPlatform() && CompareTag("Ghost"))
        {
            Invoke("SpawnDelayedPlatform", ghostPlatSpawnDelay);
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.platfromCreation,0.55f);
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
    
    private void DisableRenderers(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = false;
            }

            DisableRenderers(child);
        }
    }

    private void EnableRenderers(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.enabled = true;
            }

            EnableRenderers(child);
        }
    }
}

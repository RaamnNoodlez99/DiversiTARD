using System.Collections;
using System.Collections.Generic;
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
    //public string testString = "default";
    public GameObject ghostPlatform;

    bool isJumping = false;
    bool activateJump = false;
    bool startTimer = false;
    bool earlyRelease = false;
    float timer;

    Vector2 moveInput;
    RaycastHit2D[] m_Contacts = new RaycastHit2D[100];

    public bool IsMoving { get; private set; }

    Rigidbody2D playerBody;
    public Animator animator;

    private bool facingLeft = true; 

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPlatform();
        }
    }

    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(moveInput.x * walkSpeed, playerBody.velocity.y);

        if(CompareTag("Ghost") || CompareTag("WoodenMan"))
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        }
        

        if(moveInput.x > 0 && facingLeft)
        {
            Flip();
        }

        if(moveInput.x < 0 && !facingLeft)
        {
            Flip();
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

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
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


    //checking if grounded
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
        {
            isJumping = false;

            if (CompareTag("Ghost") || CompareTag("WoodenMan"))
                animator.SetBool("isJumping", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ghost Platform"))
        {
            isJumping = true;

            if (CompareTag("Ghost") || CompareTag("WoodenMan"))
                animator.SetBool("isJumping", true);
        }
    }

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
        Instantiate(ghostPlatform, spawnPosition, Quaternion.identity);
    }
}

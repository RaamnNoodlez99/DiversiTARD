using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase3 : MonoBehaviour
{
    public GameObject woodenMan;
    public float rushDistance = 10.0f; // The distance the boss should rush
    public float secondRushDistance = 10.0f; // The distance the boss should rush
    public float rushSpeed = 5.0f; // Adjust the speed at which the boss rushes
    public float cooldownTime = 2.0f; // Cooldown time in seconds
    public bool isRushing = false;
    private bool hitSomething = false;
    private bool isOnCooldown = false;
    private string direction = "left";
    public AudioSource bossRoar;
    public int bossDamage;
    public AudioSource bossHurt;


    public GameObject bossObject;
    private Animator bossObjectAnimator;
    private bool isFlickering;
    private bool waitForLoad = true;
    private bool hasTeleported = false;
    private int typeRush = 1;

    private bool facingLeft = false;


    public Animator auraAnimator;
    private bool isAboutToAttack = false;
    private float startTime;
    public float aboutToAttackTime = 1f;
    
    private void Start()
    {
        isOnCooldown = true;
        bossObjectAnimator = bossObject.GetComponent<Animator>();
        StartCoroutine(StartCooldown());
    }
    
    

    private void Update()
    {
        if (woodenMan.transform.position.x > gameObject.transform.position.x)
        {
            direction = "right";

            if (!facingLeft && !isRushing)
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;
                facingLeft = !facingLeft;
            }
        }
        else
        {
            direction = "left";

            if (facingLeft && !isRushing)
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;
                facingLeft = !facingLeft;
            }
        }

        if(!isOnCooldown && !isRushing)
        {
            startTime = Time.time;
            auraAnimator.SetBool("aboutToAttack", false);
            StartCoroutine(StartRush(direction));
        }

    }


    IEnumerator StartRush(string direction)
    {
        float currentRushDist = 0;
        if(typeRush == 1)
        {
            currentRushDist = rushDistance;
            typeRush = 2;
        }
        else
        {
            currentRushDist = secondRushDistance;
            typeRush = 1;
        }

        hitSomething = false;
        isRushing = true;
        bossRoar.Play();
        bossObjectAnimator.SetBool("isRushing", true);
        
        bossObjectAnimator.SetTrigger("startStomp");
        
        yield return new WaitForSeconds(1f);


        Vector3 targetPosition;
        if (direction == "right")
        {
             targetPosition = new Vector3(transform.position.x + currentRushDist, transform.position.y, transform.position.z);
        }
        else
        {
             targetPosition = new Vector3(transform.position.x - currentRushDist, transform.position.y, transform.position.z);
        }

        if(direction == "right")
        {
            while (transform.position.x <= targetPosition.x)
            {
                if (hitSomething || hasTeleported)
                {
                    //hitBlock.SetActive(true);
                    Debug.Log("Hit Something");
                    hasTeleported = false;
                    isRushing = false;
                    isOnCooldown = true;
                    StartCoroutine(StartCooldown());
                    yield break;
                }
                // Calculate the movement direction
                Vector3 moveDirection = (targetPosition - transform.position).normalized;

                // Move the boss towards the target position with the specified rushSpeed
                transform.position += moveDirection * rushSpeed * Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            while (transform.position.x >= targetPosition.x)
            {
                if (hitSomething || hasTeleported)
                {
                    //hitBlock.SetActive(true);
                    Debug.Log("Hit Something");
                    hasTeleported = false;
                    isRushing = false;
                    isOnCooldown = true;
                    StartCoroutine(StartCooldown());
                    yield break;
                }
                // Calculate the movement direction
                Vector3 moveDirection = (targetPosition - transform.position).normalized;

                // Move the boss towards the target position with the specified rushSpeed
                transform.position += moveDirection * rushSpeed * Time.deltaTime;

                yield return null;
            }
        }

        isOnCooldown = true;
        isRushing = false;

       // stopBlock.SetActive(true);

        StartCoroutine(StartCooldown());
        yield return null;
    }
    

    IEnumerator StartCooldown()
    {

        if (waitForLoad)
        {
            waitForLoad = false;
            yield return new WaitForSeconds(2.0f);
        }

        bossObjectAnimator.SetBool("isRushing", false);

        //Boss standing still before rushing again

        while (Time.time - startTime < cooldownTime)
        {
            if (Time.time - startTime >= aboutToAttackTime)
            {
                auraAnimator.SetBool("aboutToAttack", true);
            }

            yield return null;
        }


        // yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player_Controller player = null;
        if (collision.gameObject.CompareTag("WoodenMan"))
        {
            player = GameObject.FindGameObjectWithTag("WoodenMan").GetComponent<Player_Controller>();
        }

        if (player != null)
        {
            player.knockbackCounter = player.knockbackTotalTime;
            if(SFX_Manager.sfxInstance.bossLaugh != null)
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.bossLaugh);

            if (collision.transform.position.x <= transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }

            Health objectHealth = collision.collider.GetComponent<Health>();
            objectHealth.RemoveLife();
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            bossHurt.Play();
        }
        else
        {
            if(!collision.gameObject.CompareTag("Platform") && !collision.gameObject.CompareTag("Portal"))
            {
                hitSomething = true;
                if (facingLeft)
                {
                    Vector3 newPosition = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
                    transform.position = newPosition;
                }
                else
                {
                    Vector3 newPosition = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
                    transform.position = newPosition;
                }

            }
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

    IEnumerator Flicker(Transform parentTransform)
    {
        isFlickering = true;

        for (int i = 0; i < 5; i++)
        {
            EnableRenderers(parentTransform);
            yield return new WaitForSeconds(0.1f);
            DisableRenderers(parentTransform);
            yield return new WaitForSeconds(0.1f);
        }

        EnableRenderers(parentTransform);
        isFlickering = false;
    }

    public void flickerBoss()
    {
        if (!isFlickering)
            StartCoroutine(Flicker(gameObject.transform));
    }

    public void HasTelported()
    {
        hasTeleported = true;
    }
}

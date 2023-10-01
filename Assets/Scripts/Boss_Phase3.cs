using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase3 : MonoBehaviour
{
    public GameObject woodenMan;
    public float rushDistance = 10.0f; // The distance the boss should rush
    public float rushSpeed = 5.0f; // Adjust the speed at which the boss rushes
    public float cooldownTime = 2.0f; // Cooldown time in seconds
    private bool isRushing = false;
    private bool hitSomething = false;
    private bool isOnCooldown = false;
    private string direction = "left";
    public AudioSource bossRoar;

    public GameObject bossObject;
    private Animator bossObjectAnimator;

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
        Debug.Log(direction);

        if(!isOnCooldown && !isRushing)
        {
            startTime = Time.time;
            auraAnimator.SetBool("aboutToAttack", false);
            StartCoroutine(StartRush(direction));
        }

    }

    IEnumerator StartRush(string direction)
    {
        hitSomething = false;
        isRushing = true;
        bossRoar.Play();
        bossObjectAnimator.SetBool("isRushing", true);

        Vector3 targetPosition;
        if (direction == "right")
        {
             targetPosition = new Vector3(transform.position.x + rushDistance, transform.position.y, transform.position.z);
        }
        else
        {
             targetPosition = new Vector3(transform.position.x - rushDistance, transform.position.y, transform.position.z);
        }

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.01f)
        {
            if (hitSomething)
            {
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

        isOnCooldown = true;
        isRushing = false;

        StartCoroutine(StartCooldown());
        yield return null;
    }
    

    IEnumerator StartCooldown()
    {
        //Boss standing still before rushing again
        bossObjectAnimator.SetBool("isRushing", false);

        while (Time.time - startTime < cooldownTime)
        {
            if (Time.time - startTime >= aboutToAttackTime)
            {
                Debug.Log("aboutToAttack");
                auraAnimator.SetBool("aboutToAttack", true);
            }

            yield return null;
        }
        
        // yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitSomething = true;
    }

    public void flicker()
    {

    }
}

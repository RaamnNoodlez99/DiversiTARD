using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;

    public Transform woodenMan;
    public float agroRange;
    public Transform castPoint;

    private Rigidbody2D _rb;

    private Transform currentPoint;
    private bool isFacingLeft = true;
    private Animator _animator;

    private bool doBounceOnce = false;
    private bool inAnimation = false;

    private CapsuleCollider2D _capsuleCollider2D;
    private CircleCollider2D _circleCollider2D;
    private Health _health;
    private float oldHealth;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _health = GetComponent<Health>();
        oldHealth = _health.health;
        currentPoint = pointB.transform;
    }
    
    void Update()
    {
        if(woodenMan != null)
        {
            float distToPlayer = Vector2.Distance(transform.position, woodenMan.position);
            pointA.transform.position = new Vector3(pointA.transform.position.x, transform.position.y, 0);
            pointB.transform.position = new Vector3(pointB.transform.position.x, transform.position.y, 0);

            // if (canSeePlayer(agroRange))
            // {
            //     chasePlayer();
            // }
            if (distToPlayer < agroRange)
            {
                chasePlayer();
            }
            else
            {
                patrol();
            }

            if (_health.health < oldHealth)
            {
                StartCoroutine(Stun(2f));
                oldHealth = _health.health;
            }
        }
    }
    
    private void chasePlayer()
    {
        agroRange = Single.PositiveInfinity;
        speed = 9;

        if (!doBounceOnce)
        {
            StartCoroutine(Bounce(1f));
        }

        if (!inAnimation)
        {
            if (transform.position.x - woodenMan.position.x <= 0.2f &&
                transform.position.x - woodenMan.position.x >= -0.2f)
            {
                //do nothing
                _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
            else if (transform.position.x < woodenMan.position.x)
            {
                _rb.constraints = RigidbodyConstraints2D.None;
                //enemy is to the left side of the player, move right
                _rb.velocity = new Vector2(speed, 0);
                transform.localScale = new Vector2(0.8f, transform.localScale.y);
            }
            else if(transform.position.x > woodenMan.position.x)
            {
                _rb.constraints = RigidbodyConstraints2D.None;
                //enemy is to the right side of the player, move left
                _rb.velocity = new Vector2(-speed, 0);
                transform.localScale = new Vector2(-0.8f, transform.localScale.y);
            }
        }
    }

    private IEnumerator Stun(float duration)
    {
        float elapsed = 0f;
        if (elapsed == 0f)
        {
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                if (transform.position.x < woodenMan.position.x)
                {
                    transform.localScale = new Vector3(-0.8f, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(0.8f, transform.localScale.y, transform.localScale.z);
                }

                
                _animator.SetBool("isRolling", false);
                transform.rotation = Quaternion.identity;
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                yield return null;
            }
        }
        
        _animator.SetBool("isRolling", true);
    }

    private void patrol()
    {
        if (currentPoint == pointB.transform)
        {
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
            transform.localScale = new Vector2(-0.8f, transform.localScale.y);
            isFacingLeft = false;
        }
        else
        {
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
            transform.localScale = new Vector2(0.8f, transform.localScale.y);
            isFacingLeft = true;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint.position == pointB.transform.position)
        {
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint.position == pointA.transform.position)
        {
            currentPoint = pointB.transform;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }
    
    private IEnumerator Bounce(float duration)
    {
        inAnimation = true;
        
        _animator.SetBool("isRolling", true);

        _capsuleCollider2D.enabled = false;
        _circleCollider2D.enabled = true;

        _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        
        _rb.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
       //_rb.velocity = new Vector2(_rb.velocity.x, 25);

        doBounceOnce = true;
        
        yield return new WaitForSeconds(duration);

        inAnimation = false;
        _rb.constraints = RigidbodyConstraints2D.None;
    }
    
    
    
    //UNUSED FUNCTIONS:
    bool canSeePlayer(float distance)
    {
        bool val = false;

        float castDist = distance;

        if (isFacingLeft)
        {
            Debug.Log("Left facing");
            castDist = -distance;
        }
        else
        {
            Debug.Log("right facing");
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("WoodenMan"));
        
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("WoodenMan"))
            {
                val = true;
            }
            
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return val;
    }
}

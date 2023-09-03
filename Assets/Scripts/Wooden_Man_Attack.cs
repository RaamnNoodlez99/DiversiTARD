using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wooden_Man_Attack : MonoBehaviour
{
    public Animator animator;
    public float timeToAttack = 1f;

    private bool soundAlreadyPlayed = false;

    float timer = 0;
    bool isAttacking = false;
    GameObject attackArea = default;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (isAttacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack - 0.55 && !soundAlreadyPlayed)
            {
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.axeSwing);
                soundAlreadyPlayed = true;
            }

            if (timer >= timeToAttack)
            {
                soundAlreadyPlayed = false;
                timer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
                animator.SetBool("isAttacking", false);
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        StartCoroutine(activateAttackCollider(0.85f));
    }

    private IEnumerator activateAttackCollider(float duration)
    {
        yield return new WaitForSeconds(duration);
        attackArea.SetActive(isAttacking);
    }

}

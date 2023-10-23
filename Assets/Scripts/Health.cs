using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
     public int health = 100;
    // public int MAX_HEALTH = 100;
    public GameObject damageFlickerer;
    //
    // public HealthBar healthBar;
    
    
    
    Stack<GameObject> livesStack = new Stack<GameObject>();
    public Transform lives;

    public void Awake()
    {
        // if(gameObject.CompareTag("WoodenMan"))
        //     healthBar.setMaxHealth(300);
        
        
        if (lives != null)
        {
            for (int i = 0; i < lives.childCount; i++)
            {
                Transform life = lives.GetChild(i);
                livesStack.Push(life.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("Could not find the 'Lives' GameObject.");
        }
    }

    // public void Damage(int amount)
    // {
    //     if (amount < 0)
    //     {
    //         throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
    //     }
    //
    //     health -= amount;
    //
    //     if(healthBar != null)
    //         healthBar.setHealth(health);
    //
    //     if(health <= 0)
    //     {
    //         Die();
    //     }
    //     else if(damageFlickerer != null)
    //     {
    //         damageFlickerer.GetComponent<Damage_Flicker>().Flicker();
    //     }
    //
    //     if (gameObject.CompareTag("WoodenMan"))
    //         SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.painGrunt);
    // }
    
    private bool isInvincible = false;

    public void RemoveLife()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());
            Destroy(livesStack.Pop());

            if (livesStack.Count == 0)
            {
                Die();
            } else if(damageFlickerer != null)
            {
                damageFlickerer.GetComponent<Damage_Flicker>().Flicker();
            }
        
            if (gameObject.CompareTag("WoodenMan"))
                SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.painGrunt);
        }
    }
    
    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.7f);
        isInvincible = false;
    }
  

    // public void Heal(int amount)
    // {
    //     if(amount < 0)
    //     {
    //         throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
    //     }
    //
    //     bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;
    //
    //     if (wouldBeOverMaxHealth)
    //     {
    //         health = MAX_HEALTH;
    //     }
    //     else
    //     {
    //         health += amount;
    //     }
    //    
    // }

    public void Die()
    {
        Debug.Log("I am dead");

        if (gameObject.CompareTag("WoodenMan"))
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.deathGrunt);

        Destroy(gameObject);
    }
}

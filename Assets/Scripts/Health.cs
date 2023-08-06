using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int MAX_HEALTH = 100;

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        health -= amount;

        if(health <= 0)
        {
            Die();
        }

        if(gameObject.CompareTag("WoodenMan"))
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.painGrunt);
    }

    public void Heal(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            health = MAX_HEALTH;
        }
        else
        {
            health += amount;
        }
       
    }

    public void Die()
    {
        Debug.Log("I am dead");

        if (gameObject.CompareTag("WoodenMan"))
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.deathGrunt);

        Destroy(gameObject);
    }
}

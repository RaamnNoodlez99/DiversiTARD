using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int damage = 30;
    public GameObject woodenMan;

    private Health _playerHealth;
    
    private void Awake()
    {
        _playerHealth = woodenMan.GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodenMan"))
        {
            _playerHealth.RemoveLife();
        }
    }
}

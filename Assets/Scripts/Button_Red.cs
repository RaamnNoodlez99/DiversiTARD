using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Red : MonoBehaviour
{
    public GameObject linkedTorch;

    private GameObject torchesFlame;
    private Animator _animator;
    private Button buttonComponent;

    void Start()
    {
        buttonComponent = GetComponent<Button>();
        _animator = this.gameObject.GetComponent<Animator>();

        if (linkedTorch != null && linkedTorch.transform.childCount > 0)
        {
            torchesFlame = linkedTorch.transform.GetChild(0).gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") || other.CompareTag("Projectile"))
        {
            _animator.SetBool("isPressed", true);
            if (linkedTorch != null)
                torchesFlame.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") || other.CompareTag("Projectile"))
        { 
            buttonComponent.onClick.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") || other.CompareTag("Projectile"))
        {
            _animator.SetBool("isPressed", false);
            if (linkedTorch != null)
                torchesFlame.SetActive(false);


        }
    }
}

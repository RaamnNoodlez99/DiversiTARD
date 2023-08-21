using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Red : MonoBehaviour
{
    public GameObject linkedTorch;

    private GameObject torchesFlame;
    private Animator _animator;
    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();

        if (linkedTorch.transform.childCount > 0)
        {
            torchesFlame = linkedTorch.transform.GetChild(0).gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan"))
        {
            _animator.SetBool("isPressed", true);
            torchesFlame.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan"))
        {
            _animator.SetBool("isPressed", false);
            torchesFlame.SetActive(false);
        }
    }
}

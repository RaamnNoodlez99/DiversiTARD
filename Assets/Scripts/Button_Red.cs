using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Red : MonoBehaviour
{
    public GameObject linkedTorch;
    public float buttonPressedTime;
    public bool isPopUpButton = true;

    private GameObject torchesFlame;
    private Animator _animator;
    private Button buttonComponent;
    private GameObject characterCheck;
    
    public Color dadButtonColor;
    public Color dadPressedButtonColor;
    public Color dadCupColor;
    public Color ghostButtonColor;
    public Color ghostPressedButtonColor;
    public Color ghostCupColor;

    void Start()
    {
        characterCheck = GameObject.Find("Ghost");
        buttonComponent = GetComponent<Button>();
        GameObject buttonChild = this.transform.GetChild(0).gameObject;
        _animator = buttonChild.gameObject.GetComponent<Animator>();

        if (linkedTorch != null && linkedTorch.transform.childCount > 0)
        {
            torchesFlame = linkedTorch.transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        if (characterCheck != null)
        {
            GameObject buttonGameObject = transform.GetChild(0).gameObject;
            SpriteRenderer buttonSpriteRenderer = buttonGameObject.GetComponent<SpriteRenderer>();
                            
            GameObject cupGameObject = transform.GetChild(1).gameObject;
            SpriteRenderer cupSpriteRenderer = cupGameObject.GetComponent<SpriteRenderer>();

            if (characterCheck.GetComponent<Character_Switch>().getCurCharacter() == "WoodenMan")
            {
                buttonSpriteRenderer.color = dadButtonColor;
                cupSpriteRenderer.color = dadCupColor;

                if (_animator.GetBool("isPressed"))
                    buttonSpriteRenderer.color = dadPressedButtonColor;
            }
            else
            {
                buttonSpriteRenderer.color = ghostButtonColor;
                cupSpriteRenderer.color = ghostCupColor;
                
                if (_animator.GetBool("isPressed"))
                    buttonSpriteRenderer.color = ghostPressedButtonColor;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") && !_animator.GetBool("isPressed"))
        {
            _animator.SetBool("isPressed", true);
            if (linkedTorch != null)
                torchesFlame.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") || other.CompareTag("StoneBall"))
        {
            buttonComponent.onClick.Invoke();
        }

        if (other.CompareTag("StoneBall") && !_animator.GetBool("isPressed"))
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.stoneShort);

            _animator.SetBool("isPressed", true);
            if (linkedTorch != null)
                torchesFlame.SetActive(true);

            if(isPopUpButton)
                StartCoroutine(PopUpButton());
        }

        if (other.CompareTag("WoodenMan") && !_animator.GetBool("isPressed"))
        {
            SFX_Manager.sfxInstance.Audio.PlayOneShot(SFX_Manager.sfxInstance.stoneShort);
        }
    }

    private IEnumerator PopUpButton()
    {
        yield return new WaitForSeconds(buttonPressedTime);
        _animator.SetBool("isPressed", false);
        if (linkedTorch != null)
            torchesFlame.SetActive(false);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("WoodenMan") && isPopUpButton)
        {
            StartCoroutine(PopUpButton());
        }
    }
}

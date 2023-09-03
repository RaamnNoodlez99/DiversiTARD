using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Input_Device_Manager : MonoBehaviour
{
    private string lastInputDevice = "";

    bool isKeyboardInput;
    bool isControllerInput;

    public SpriteRenderer switchRenderer;
    public Sprite switchGamepad;
    public Sprite switchKeyboard;

    public SpriteRenderer platformCreationRenderer;
    public Sprite platformGamepad;
    public Sprite platformKeyboard;

    public SpriteRenderer attackRenderer;
    public Sprite attackGamepad;
    public Sprite attackKeyboard;

    public Image buttonRenderer;
    public Sprite gamepadSprite;
    public Sprite keyboardSprite;


    private void Awake()
    {
        if(PlayerPrefs.GetInt("keyboardLastInput") == 1)
        {
            if (switchRenderer != null)
            {
                switchRenderer.sprite = switchKeyboard;
            }
            if (platformCreationRenderer != null)
            {
                platformCreationRenderer.sprite = platformKeyboard;
            }
            if (attackRenderer != null)
            {
                attackRenderer.sprite = attackKeyboard;
            }
            if(buttonRenderer != null)
            {
                buttonRenderer.sprite = keyboardSprite;
            }
        }
        else
        {
            if (switchRenderer != null)
            {
                switchRenderer.sprite = switchGamepad;
            }
            if (platformCreationRenderer != null)
            {
                platformCreationRenderer.sprite = platformGamepad;
            }
            if (attackRenderer != null)
            {
                attackRenderer.sprite = attackGamepad;
            }
            if(buttonRenderer != null)
            {
                buttonRenderer.sprite = gamepadSprite;
            }
        }
    }
    void Update()
    {
       
        if (Keyboard.current.anyKey.wasPressedThisFrame || (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            isKeyboardInput = true;
            isControllerInput = false;
            PlayerPrefs.SetInt("keyboardLastInput", 1);
        }


        if (Gamepad.current != null)
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad.leftStick.ReadValue().sqrMagnitude > 0 ||
                gamepad.rightStick.ReadValue().sqrMagnitude > 0 ||
                gamepad.leftTrigger.ReadValue() > 0 ||
                gamepad.rightTrigger.ReadValue() > 0 ||
                gamepad.leftShoulder.isPressed ||
                gamepad.rightShoulder.isPressed ||
                gamepad.buttonNorth.isPressed ||
                gamepad.buttonSouth.isPressed ||
                gamepad.buttonWest.isPressed ||
                gamepad.buttonEast.isPressed ||
                gamepad.selectButton.isPressed || 
                gamepad.startButton.isPressed)
            {
                isControllerInput = true;
                isKeyboardInput = false;
                PlayerPrefs.SetInt("keyboardLastInput", 0);
            }
        }

        if (isKeyboardInput)
        {
            if(switchRenderer != null)
            {
                switchRenderer.sprite = switchKeyboard;
            }
            if(platformCreationRenderer != null)
            {
                platformCreationRenderer.sprite = platformKeyboard;
            }
            if(attackRenderer != null)
            {
                attackRenderer.sprite = attackKeyboard;
            }
            if (buttonRenderer != null)
            {
                buttonRenderer.sprite = keyboardSprite;
            }
        }
        else if (isControllerInput)
        {
            if (switchRenderer != null)
            {
                switchRenderer.sprite = switchGamepad;
            }
            if (platformCreationRenderer != null)
            {
                platformCreationRenderer.sprite = platformGamepad;
            }
            if (attackRenderer != null)
            {
                attackRenderer.sprite = attackGamepad;
            }
            if (buttonRenderer != null)
            {
                buttonRenderer.sprite = gamepadSprite;
            }
        }
    }
}

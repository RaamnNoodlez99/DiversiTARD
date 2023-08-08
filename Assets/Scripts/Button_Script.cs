using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Script : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    private Button button;
    public AudioSource buttonSource;
    public AudioClip buttonMove;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSource.PlayOneShot(buttonMove);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonSource.PlayOneShot(buttonMove);
    }
}

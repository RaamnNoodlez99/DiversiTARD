using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter_Controller : MonoBehaviour
{
    public float startPosition;
    public float endPosition;
    public float movementSpeed;

    private void OnEnable()
    {
        Vector3 initialPosition = new Vector3(startPosition, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = initialPosition;
        gameObject.GetComponent<Poison_Vat>().StopDropping();
        gameObject.GetComponent<Poison_Vat>().StartDropping();

        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        if (endPosition > startPosition)
        {
            while (transform.position.x < endPosition)
            {
                Vector3 newPosition = new Vector3(gameObject.transform.position.x + movementSpeed, gameObject.transform.position.y, gameObject.transform.position.z);

                gameObject.transform.position = newPosition;

                yield return null;
            }
            gameObject.GetComponent<Poison_Vat>().StopDropping();
            gameObject.SetActive(false);
        }
        else
        {
            while (transform.position.x > endPosition)
            {
                Vector3 newPosition = new Vector3(gameObject.transform.position.x - movementSpeed, gameObject.transform.position.y, gameObject.transform.position.z);

                gameObject.transform.position = newPosition;

                yield return null;
            }
            gameObject.GetComponent<Poison_Vat>().StopDropping();
            gameObject.SetActive(false);
        }
    }
}

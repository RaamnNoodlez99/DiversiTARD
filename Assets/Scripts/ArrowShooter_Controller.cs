using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter_Controller : MonoBehaviour
{
    public float startPosition;
    public float endPosition;
    public float movementSpeed;

    private Vector3 targetPosition;

    private void OnEnable()
    {
        Vector3 initialPosition = new Vector3(startPosition, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = initialPosition;
        gameObject.GetComponent<Poison_Vat>().StopDropping();
        gameObject.GetComponent<Poison_Vat>().StartDropping();

        targetPosition = new Vector3(endPosition, transform.position.y, transform.position.z);

        StartCoroutine(MoveObject());
    }

    private IEnumerator MoveObject()
    {
        if (endPosition > startPosition)
        {
            while (transform.position.x < endPosition)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * movementSpeed * Time.deltaTime;
                yield return null;
            }
            gameObject.GetComponent<Poison_Vat>().StopDropping();
            gameObject.SetActive(false);
        }
        else
        {
            while (transform.position.x > endPosition)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * movementSpeed * Time.deltaTime;
                yield return null;
            }
            gameObject.GetComponent<Poison_Vat>().StopDropping();
            gameObject.SetActive(false);
        }
    }
}

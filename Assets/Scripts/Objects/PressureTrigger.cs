using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressureTrigger : MonoBehaviour
{
    //Audio
    [SerializeField]
    private AudioSource onPress;
    [SerializeField]
    private AudioSource onRelease;

    [SerializeField]
    private Vector3 movementOnPress;
    [SerializeField]
    private bool beingPressed;
    [SerializeField]
    private float movementPerTick;

    private Vector3 startPosition;
        
    //Events
    public UnityEvent onPressEvent;
    public UnityEvent onReleaseEvent;

    private IEnumerator movementRoutine;

    private void Start()
    {
        startPosition = transform.position;
        movementRoutine = Movement();
        StartCoroutine(movementRoutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            beingPressed = true;
            onPressEvent.Invoke();
            if (onPress)
                onPress.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            beingPressed = false;
            onReleaseEvent.Invoke();
            if (onRelease)
                onRelease.Play();
        }
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            if (beingPressed)
            {
                if (transform.position.y > (startPosition.y + movementOnPress.y))
                {
                    transform.Translate(movementOnPress * movementPerTick);
                }
            }
            else
            {
                if (transform.position.y <= startPosition.y)
                {
                    transform.Translate(movementOnPress * -movementPerTick);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

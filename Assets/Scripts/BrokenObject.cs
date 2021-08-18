using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public GameObject cameraFreeViewPosition;
    Animator animator;


    private Vector3 initialMousePosition, deltaMousePosition,initialPosition;
    private Quaternion initialRotation;
    private bool isRotationActivated = false;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        animator = GetComponent<Animator>();
        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventsManager_OnSpeechBubbleClicked);
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelExitButtonClicked);
    }


    private void EventsManager_OnSpeechBubbleClicked(BrokenObject _brokenObject)
    {
        if(this == _brokenObject)
        {
            isRotationActivated = true;
            animator.SetTrigger("Selected");
        }
    }

    void Update()
    {
        if (isRotationActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                deltaMousePosition = Input.mousePosition - initialMousePosition;
                Vector3 mouseVector = new Vector3(deltaMousePosition.y, -deltaMousePosition.x, 0);
                transform.RotateAround(transform.position, Camera.main.transform.right, mouseVector.x * Time.deltaTime);
                transform.RotateAround(transform.position, Camera.main.transform.up, mouseVector.y * Time.deltaTime);
            }
        }
    }

    private void EventManager_OnLevelExitButtonClicked(BrokenObject _brokenObject)
    {
        if(this == _brokenObject)
        {
            isRotationActivated = false;
            animator.SetTrigger("NotSelected");
            StartCoroutine(GoToDefaultPositionCo());
        }
    }

    IEnumerator GoToDefaultPositionCo()
    {
        float duration = 0f;
        float maxDuration = 1f;
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;


        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, initialPosition, (duration / maxDuration));
            transform.rotation = Quaternion.Lerp(currentRotation, initialRotation, (duration / maxDuration));
            yield return null;
        }
    }
}

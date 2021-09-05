using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    Vector3 deltaMousePosition;
    Vector3 diffvec;

    Vector3 initialPosition;
    Quaternion initialRotation;

    float rotationMotionTime;
    [SerializeField]bool inAnimatorState = true;
    [SerializeField] float minYRot, maxYRot;


    private void Awake()
    {
        rotationMotionTime = 0.5f;
        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventManager_OnSpeechBubbleClicked);
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelSelectorExitButtonClicked);
    }

    void Update()
    {
        if (inAnimatorState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                deltaMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                diffvec = Input.mousePosition - deltaMousePosition;
                deltaMousePosition = Input.mousePosition;
                rotationMotionTime += diffvec.x * Time.deltaTime / 5;
                rotationMotionTime = Mathf.Clamp01(rotationMotionTime);

                Vector3 rot = transform.rotation.eulerAngles;
                rot.y = Mathf.Lerp(minYRot, maxYRot, rotationMotionTime);
                transform.rotation = Quaternion.Euler(rot);
            }
        }
    }

    public void GoToTransform(Transform goTransform)
    {
        StartCoroutine(ZoomToPositionCo(goTransform));
    }

    public void ReturnToDefaultTransform()
    {
        StartCoroutine(ReturnToDefaultTransformCo());
    }

    IEnumerator ZoomToPositionCo(Transform goTransform)
    {
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;

        float duration = 0f;
        float maxDuration = 1f;
        inAnimatorState = false;

        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPosition, goTransform.position, (duration / maxDuration));
            transform.rotation = Quaternion.Lerp(initialRotation, goTransform.rotation, (duration / maxDuration));
            yield return null;
        }
    }

    IEnumerator ReturnToDefaultTransformCo()
    {
        Vector3 currentPosition = gameObject.transform.position;
        Quaternion currentRotation = gameObject.transform.rotation;

        

        float duration = 0f;
        float maxDuration = 1f;
        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPosition, initialPosition, (duration/maxDuration));
            transform.rotation = Quaternion.Lerp(currentRotation, initialRotation, (duration / maxDuration));
            yield return null;
        }
        inAnimatorState = true;
    }

    private void EventManager_OnSpeechBubbleClicked(BrokenObject brokenObject,SpeechBubble speechBubble)
    {
        GoToTransform(brokenObject.cameraFreeViewPosition.transform);
    }

    private void EventManager_OnLevelSelectorExitButtonClicked(BrokenObject _brokenObject,SpeechBubble speechBubble)
    {
        ReturnToDefaultTransform();
    }
}

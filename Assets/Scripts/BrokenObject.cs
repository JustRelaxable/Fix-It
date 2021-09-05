using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BrokenObject : MonoBehaviour
{
    public GameObject cameraFreeViewPosition;
    [SerializeField] NotifyingObject[] _notifyingObject;
    Animator animator;


    private Vector3 initialMousePosition, deltaMousePosition,initialLocalPosition;
    private Quaternion initialLocalRotation;
    private bool isRotationActivated = false;
    bool glueSceneFinished = false;

    public bool isFixed = false;

    private void Awake()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        animator = GetComponent<Animator>();
        animator.keepAnimatorControllerStateOnDisable = true;
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelExitButtonClicked);
        EventManager.instance.OnGlueSceneFinished.AddListener(EventManager_OnGlueSceneFinished);
    }

    private void Shake()
    {
        transform.DOShakeRotation(10f, 10f, 1, 50f, true);
    }

    public void SelectBrokenObject()
    {
        isRotationActivated = true;
        animator.SetTrigger("Selected");
        foreach (var item in _notifyingObject)
        {
            item.EnableMeshRenderer();
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

    private void EventManager_OnLevelExitButtonClicked(BrokenObject _brokenObject,SpeechBubble speechBubble)
    {
    }

    public void HandleOnLevelExitButtonClicked()
    {
        isRotationActivated = false;
        if (!glueSceneFinished)
        {
            animator.SetTrigger("NotSelected");
        }
        else
        {
            glueSceneFinished = false;
        }
        StartCoroutine(GoToDefaultPositionCo());
    }

    private void EventManager_OnGlueSceneFinished(float score,BrokenObject _brokenObject)
    {
        glueSceneFinished = true;
    }

    public void FixTheObject()
    {
        animator.SetTrigger("Fixing");
        Shake();
        foreach (var notifyingObject in _notifyingObject)
        {
            notifyingObject.DisableMeshRenderer();
        }
        isFixed = true;
    }

    IEnumerator GoToDefaultPositionCo()
    {
        float duration = 0f;
        float maxDuration = 1f;
        Vector3 currentPosition = transform.localPosition;
        Quaternion currentRotation = transform.localRotation;


        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(currentPosition, initialLocalPosition, (duration / maxDuration));
            transform.localRotation = Quaternion.Lerp(currentRotation, initialLocalRotation, (duration / maxDuration));
            yield return null;
        }
    }
}

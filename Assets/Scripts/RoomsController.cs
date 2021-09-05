using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : GenericSingleton<RoomsController>
{
    private Vector3 differenceVector,inverseDifferenceVector,translationVector;
    [SerializeField] GameObject room2;
    [SerializeField] AnimationCurve animationCurve;
    public bool ableToChangeRoom = true;
    private int _roomIndex = 0;
    private int _roomCount;

    protected override void Awake()
    {
        base.Awake();
        differenceVector = transform.position - room2.transform.position;
        inverseDifferenceVector = -differenceVector;
        _roomCount = transform.childCount;

        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventsManager_OnSpeechBubbleClicked);
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventsManager_OnLevelSelectorExitButtonClicked);
    }

    IEnumerator ChangeRoomCo(int leftOrRight)
    {
        float duration = 0f;
        float maxDuration = 1f;

        if(leftOrRight == 0)
        {
            translationVector = inverseDifferenceVector;
            _roomIndex--;
        }
        else
        {
            translationVector = differenceVector;
            _roomIndex++;
        }
        Vector3 currentPos = transform.position;

        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            float percentage = animationCurve.Evaluate((duration / maxDuration));
            transform.position = Vector3.LerpUnclamped(currentPos, currentPos + translationVector, percentage);
            yield return null;
        }
    }

    public void ChangeRoom(int leftOrRight)
    {
        if (ableToChangeRoom && CanChangeRoom(leftOrRight))
        {
            StartCoroutine(ChangeRoomCo(leftOrRight));
        }
    }

    private void EventsManager_OnSpeechBubbleClicked(BrokenObject brokenObject,SpeechBubble speechBubble)
    {
        ableToChangeRoom = false;
    }

    private void EventsManager_OnLevelSelectorExitButtonClicked(BrokenObject brokenObject,SpeechBubble speechBubble)
    {
        ableToChangeRoom = true;
    }

    private bool CanChangeRoom(int leftOrRight)
    {
        switch (leftOrRight)
        {
            case 0:
                if (_roomIndex > 0)
                    return true;
                else
                    return false;
            case 1:
                if (_roomIndex < _roomCount-1)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : GenericSingleton<RoomsController>
{
    private Vector3 differenceVector,inverseDifferenceVector,translationVector;
    [SerializeField] GameObject room2;
    [SerializeField] AnimationCurve animationCurve;
    public bool ableToChangeRoom = true;


    protected override void Awake()
    {
        base.Awake();
        differenceVector = transform.position - room2.transform.position;
        inverseDifferenceVector = -differenceVector;

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
        }
        else
        {
            translationVector = differenceVector;
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
        if (ableToChangeRoom)
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
}

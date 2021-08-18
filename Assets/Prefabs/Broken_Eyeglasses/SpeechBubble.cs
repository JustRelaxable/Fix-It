using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] GameObject bubbleImage;
    [SerializeField] BrokenObject brokenObject;

    private void Awake()
    {
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelSelectorExitButtonClicked);
    }

    public void OnSpeechBubbleClicked()
    {
        EventManager.instance.OnSpeechBubbleClicked.Invoke(brokenObject);
        ChangeBubbleImageScale(Vector3.zero);
    }

    public void ChangeBubbleImageScale(Vector3 _scale)
    {
        StartCoroutine(ChangeBubbleImageScaleCo(_scale));
    }

    IEnumerator ChangeBubbleImageScaleCo(Vector3 _scale)
    {
        float duration = 0f;
        float maxDuration = 1f;
        Vector3 currentScale = bubbleImage.transform.localScale;

        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            bubbleImage.transform.localScale = Vector3.Lerp(currentScale, _scale, (duration / maxDuration));
            yield return null;
        }
    }

    private void EventManager_OnLevelSelectorExitButtonClicked(BrokenObject _brokenObject)
    {
        if(brokenObject == _brokenObject)
        {
            ChangeBubbleImageScale(Vector3.one);
        }
    }
}

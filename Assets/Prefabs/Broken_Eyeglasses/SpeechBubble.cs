using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] GameObject _bubbleImage;
    [SerializeField] BrokenObject _brokenObject;

    private void Awake()
    {
       // EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelSelectorExitButtonClicked);
    }

    public void OnSpeechBubbleClicked()
    {
        EventManager.instance.OnSpeechBubbleClicked.Invoke(_brokenObject,this);
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
        Vector3 currentScale = _bubbleImage.transform.localScale;

        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            _bubbleImage.transform.localScale = Vector3.Lerp(currentScale, _scale, (duration / maxDuration));
            yield return null;
        }
    }

    private void EventManager_OnLevelSelectorExitButtonClicked(BrokenObject brokenObject,SpeechBubble speechBubble)
    {
        brokenObject.HandleOnLevelExitButtonClicked();
        if (!brokenObject.isFixed)
        {
            speechBubble.ChangeBubbleImageScale(Vector3.one);
        }
        else
        {
            ChangeBubbleImageScale(Vector3.zero);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvas : MonoBehaviour
{
    Animator animator;
    BrokenObject selectedObject;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventManager_OnSpeechBubbleClicked);
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelSelectorExitButtonClicked);
    }

    private void EventManager_OnSpeechBubbleClicked(BrokenObject _brokenObject)
    {
        animator.SetTrigger("LevelSelectorOut");
        selectedObject = _brokenObject;
    }

    private void EventManager_OnLevelSelectorExitButtonClicked(BrokenObject _brokenObject)
    {
        animator.SetTrigger("LevelSelectorIn");
    }

    public void LevelSelectorExitButtonClick()
    {
        EventManager.instance.OnLevelSelectorExitButtonClicked.Invoke(selectedObject);
        selectedObject = null;
    }
}

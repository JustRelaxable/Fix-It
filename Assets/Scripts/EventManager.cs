using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : GenericSingleton<EventManager>
{
    public SpeechBubbleClicked OnSpeechBubbleClicked;
    public LevelSelectorExitButtonClicked OnLevelSelectorExitButtonClicked;
    protected override void Awake()
    {
        base.Awake();
        OnSpeechBubbleClicked = new SpeechBubbleClicked();
        OnLevelSelectorExitButtonClicked = new LevelSelectorExitButtonClicked();
    }
}

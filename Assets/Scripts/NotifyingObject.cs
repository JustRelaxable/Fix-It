using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyingObject : MonoBehaviour
{
    [SerializeField] BrokenObject brokenObject;


    private void Awake()
    {
        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventsManager_OnSpeechBubbleClicked);
    }

    private void OnMouseDown()
    {
        GameManager.instance.ActivateSahne(1);
    }

    private void EventsManager_OnSpeechBubbleClicked(BrokenObject _brokenObject)
    {
        if(brokenObject == _brokenObject)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}

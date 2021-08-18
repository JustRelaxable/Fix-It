using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : GenericSingleton<EventManager>
{
    public static OnZoom onZoom;
    private void Awake()
    {
        onZoom = new OnZoom();
    }
}

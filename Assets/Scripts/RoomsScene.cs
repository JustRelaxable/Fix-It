using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsScene : MonoBehaviour
{
    void Start()
    {
    }

    private void OnEnable()
    {
        RoomsController.instance.ableToChangeRoom = true;
    }

    void Update()
    {
        
    }
}

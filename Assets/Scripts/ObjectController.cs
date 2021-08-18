using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private Vector3 initialMousePosition;
    private Vector3 deltaMousePosition;
    public bool isRotationActivated = false;

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
                transform.RotateAround(transform.position, Camera.main.transform.up, mouseVector.y * Time.deltaTime) ;
            }
        }
    }
}

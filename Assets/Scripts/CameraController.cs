using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, IZoomable
{
    Vector3 initialPosition;
    Quaternion initialRotation;
    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void Zoom(NotifyingObject notifyingObject)
    {
        Vector3 diffVecNor = (transform.position - notifyingObject.transform.position).normalized;
        StartCoroutine(ZoomToPosition(transform.position, notifyingObject.transform.position + diffVecNor));
    }

    IEnumerator ZoomToPosition(Vector3 startPosition,Vector3 endPosition)
    {
        float duration = 0f;

        while (duration<=3f)
        {
            duration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, duration);
            yield return null;
        }
    }

    private void LateUpdate()
    {
        
    }
}

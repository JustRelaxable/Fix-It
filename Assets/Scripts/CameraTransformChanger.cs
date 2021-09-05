using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformChanger : MonoBehaviour
{
    [SerializeField] protected GameObject cameraLastPos,Button;
    [SerializeField] protected Animator animator;
    [SerializeField] protected ObjectController objectController;
    

    public void ChangeCameraLocation()
    {
        Camera.main.GetComponent<CameraAnimator>().GoToTransform(cameraLastPos.transform);
        RoomsController.instance.ableToChangeRoom = false;
        PlayAnimation();
    }

    public void CameraGoDefaultLocation()
    {
        Camera.main.GetComponent<CameraAnimator>().ReturnToDefaultTransform();
        RoomsController.instance.ableToChangeRoom = false;
    }

    public virtual void PlayAnimation()
    {

    }
}

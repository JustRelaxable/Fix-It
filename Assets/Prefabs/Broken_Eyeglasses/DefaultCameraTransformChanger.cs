using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCameraTransformChanger : CameraTransformChanger
{
    [SerializeField] GameObject notifyingObject;
    public override void PlayAnimation()
    {
        animator.SetTrigger("Selected");
        StartCoroutine(ChangeButtonScale());
        objectController.isRotationActivated = true;
        notifyingObject.SetActive(true);
    }

    IEnumerator ChangeButtonScale()
    {
        float duration = 0f;
        float maxDuration = 1f;
        Vector3 currentScale = Button.transform.localScale;

        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            Button.transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, (duration / maxDuration));
            yield return null;
        }
    }
}

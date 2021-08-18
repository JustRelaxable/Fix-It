using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsController : GenericSingleton<RoomsController>
{
    private Vector3 differenceVector,inverseDifferenceVector,translationVector;
    [SerializeField] GameObject room2;
    [SerializeField] AnimationCurve animationCurve;
    public bool isActivated = true;


    private void Start()
    {
        differenceVector = transform.position - room2.transform.position;
        inverseDifferenceVector = -differenceVector;
    }

    IEnumerator ChangeRoomCo(int leftOrRight)
    {
        float duration = 0f;
        float maxDuration = 1f;

        if(leftOrRight == 0)
        {
            translationVector = inverseDifferenceVector;
        }
        else
        {
            translationVector = differenceVector;
        }
        Vector3 currentPos = transform.position;

        while (duration <= maxDuration)
        {
            duration += Time.deltaTime;
            float percentage = animationCurve.Evaluate((duration / maxDuration));
            transform.position = Vector3.LerpUnclamped(currentPos, currentPos + translationVector, percentage);
            yield return null;
        }
    }

    public void ChangeRoom(int leftOrRight)
    {
        if (isActivated)
        {
            StartCoroutine(ChangeRoomCo(leftOrRight));
        }
    }
}

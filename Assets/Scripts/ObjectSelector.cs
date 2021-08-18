using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] Animator[] animators;

    private void OnMouseDown()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetTrigger("Selected");
        }
    }
}

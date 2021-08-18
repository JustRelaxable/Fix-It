using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyingObject : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.instance.ActivateSahne(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyingObject : MonoBehaviour
{
    [SerializeField] BrokenObject _brokenObject;

    private void OnMouseDown()
    {
        if(!_brokenObject.isFixed)
            EventManager.instance.OnGlueSceneStarted.Invoke(_brokenObject);
    }

    public void EnableMeshRenderer()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void DisableMeshRenderer()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    static public T instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<T>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

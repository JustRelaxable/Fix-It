using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueParticle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shape"))
        {
            //Debug.Log("Touched");
            GameManager.instance.Vibrate(0);

        }
        else if(!other.gameObject.CompareTag("GlueParticle"))
        {
            //Debug.Log("NotTouched");
            GameManager.instance.Vibrate(5);

        }
    }
}

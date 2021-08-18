using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueObjectMesh : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GlueParticle"))
        {
            GameManager.instance.IncreaseGlueToucned();
        }
    }
}

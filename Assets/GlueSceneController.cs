using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueSceneController : MonoBehaviour
{
    [SerializeField] GlueController glueController;
    private void OnEnable()
    {
        GameManager.instance.ResetGlueScene();
        //TODO:Randomly Select a mesh and assign
        glueController.SetMaximumGlueParticleSpawn(60);
    }
}

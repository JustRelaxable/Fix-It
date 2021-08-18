using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] GameObject[] sahneler;
    float glueSpawned, glueTouched;

    public void ActivateSahne(int sahneNumber)
    {
        for (int i = 0; i < sahneler.Length; i++)
        {
            sahneler[i].SetActive(false);
        }
        sahneler[sahneNumber].SetActive(true);
    }

    public void ResetGlueScene()
    {
        glueSpawned = 0;
        glueTouched = 0;
    }

    public void IncreaseGlueSpawned()
    {
        glueSpawned += 1;
    }

    public void IncreaseGlueToucned()
    {
        glueTouched += 1;
    }

    public void GlueOver()
    {
        float accuracy = glueTouched / glueSpawned;
        ActivateSahne(0);

    }

    public void Vibrate(int effect)
    {
        Vibrator.Vibrate(effect);
    }
}

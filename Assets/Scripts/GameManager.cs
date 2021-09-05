using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] GameObject[] sahneler;
    float glueSpawned, glueTouched;
    List<GameObject> glueParticles = new List<GameObject>();
    BrokenObject selectedBrokenObject;
    private ShapeRay _shapeRay;


    protected override void Awake()
    {
        base.Awake();
        EventManager.instance.OnGlueParticleSpawned.AddListener(EventManager_OnGlueParticleSpawned);
        EventManager.instance.OnGlueSceneStarted.AddListener(EventManager_OnGlueSceneStarted);
        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventManager_OnSpeechBubbleClicked);
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelSelectorExitButtonClicked);
        //EventManager.instance.OnGlueSceneFinished.AddListener()
        EventManager.instance.OnGlueSceneLevelFinished.AddListener(EventManager_OnGlueLevelFinished);
        EventManager.instance.OnShapeRayInitialized.AddListener(EventManager_OnShapeRayInitialized);
    }

    public void ActivateSahne(int sahneNumber)
    {
        for (int i = 0; i < sahneler.Length; i++)
        {
            sahneler[i].SetActive(false);
        }
        sahneler[sahneNumber].SetActive(true);
    }

    private void EventManager_OnGlueParticleSpawned(GameObject _glueParticle,GlueController _glueController)
    {
        glueSpawned += 1;
        glueParticles.Add(_glueParticle);
    }

    private void EventManager_OnSpeechBubbleClicked(BrokenObject brokenObject,SpeechBubble speechBubble)
    {
        brokenObject.SelectBrokenObject();
    }

    private void EventManager_OnGlueSceneStarted(BrokenObject _brokenObject)
    {
        ActivateSahne(1);
        //broken objectten gelebilir
        selectedBrokenObject = _brokenObject;
        EventManager.instance.OnGlueSceneInitialized.Invoke(60);
    }

    public void EventManager_OnGlueLevelFinished()
    {
        float accuracy = _shapeRay.GetRayPercentage();
        ResetScene();
        ActivateSahne(0);

        if(accuracy >= 0.3f)
        {
            selectedBrokenObject.FixTheObject();
        }
        EventManager.instance.OnGlueSceneFinished.Invoke(accuracy * 100,selectedBrokenObject);
    }

    private void EventManager_OnLevelSelectorExitButtonClicked(BrokenObject brokenObject, SpeechBubble speechBubble)
    {
        brokenObject.HandleOnLevelExitButtonClicked();
        if (!brokenObject.isFixed)
        {
            speechBubble.ChangeBubbleImageScale(Vector3.one);
        }
        else
        {
            speechBubble.ChangeBubbleImageScale(Vector3.zero);
        }
    }

    private void EventManager_OnShapeRayInitialized(ShapeRay shapeRay)
    {
        _shapeRay = shapeRay;
    }

    public void Vibrate(int effect)
    {
        Vibrator.Vibrate(effect);
    }

    private void ResetScene()
    {
        for (int i = 0; i < glueParticles.Count; i++)
        {
            glueParticles[i].SetActive(false);
        }
    }
}

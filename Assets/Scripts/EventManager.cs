using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : GenericSingleton<EventManager>
{
    public SpeechBubbleClicked OnSpeechBubbleClicked;
    public LevelSelectorExitButtonClicked OnLevelSelectorExitButtonClicked;
    public GlueSceneStarted OnGlueSceneStarted;
    public GlueSceneFinished OnGlueSceneFinished;
    public GlueParticleSpawned OnGlueParticleSpawned;
    public GlueSceneInitialized OnGlueSceneInitialized;
    public GlueSceneLevelFinished OnGlueSceneLevelFinished;
    public ShapeRayInitialized OnShapeRayInitialized;
    protected override void Awake()
    {
        base.Awake();
        OnSpeechBubbleClicked = new SpeechBubbleClicked();
        OnLevelSelectorExitButtonClicked = new LevelSelectorExitButtonClicked();
        OnGlueSceneStarted = new GlueSceneStarted();
        OnGlueSceneFinished = new GlueSceneFinished();
        OnGlueParticleSpawned = new GlueParticleSpawned();
        OnGlueSceneInitialized = new GlueSceneInitialized();
        OnGlueSceneLevelFinished = new GlueSceneLevelFinished();
        OnShapeRayInitialized = new ShapeRayInitialized();
    }
}

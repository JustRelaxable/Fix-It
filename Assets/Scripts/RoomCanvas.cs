using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;

public class RoomCanvas : MonoBehaviour
{
    Animator animator;
    BrokenObject selectedObject;
    SpeechBubble _selectedSpeechBubble;
    [SerializeField] Slider glueSlider;
    private ShapeRay _shapeRay;
    [SerializeField] private Text _scoreText;
    [SerializeField] AnimationClip[] starAnimationClips;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        EventManager.instance.OnSpeechBubbleClicked.AddListener(EventManager_OnSpeechBubbleClicked);
        EventManager.instance.OnLevelSelectorExitButtonClicked.AddListener(EventManager_OnLevelSelectorExitButtonClicked);
        EventManager.instance.OnGlueSceneStarted.AddListener(EventManager_OnGlueSceneStarted);
        EventManager.instance.OnGlueSceneFinished.AddListener(EventManager_OnGlueSceneFinished);
        EventManager.instance.OnGlueParticleSpawned.AddListener(EventManager_OnGlueParticleSpawned);
        EventManager.instance.OnGlueSceneInitialized.AddListener(EventManager_OnGlueSceneInitialized);
        EventManager.instance.OnShapeRayInitialized.AddListener(EventManager_OnShapeRayInitialized);
    }

    private void Start()
    {
    }

    private void EventManager_OnSpeechBubbleClicked(BrokenObject brokenObject,SpeechBubble speechBubble)
    {
        animator.SetTrigger("LevelSelectorOut");
        selectedObject = brokenObject;
        _selectedSpeechBubble = speechBubble;
    }

    private void EventManager_OnLevelSelectorExitButtonClicked(BrokenObject _brokenObject,SpeechBubble speechBubble)
    {
        animator.SetTrigger("LevelSelectorIn");
    }

    private void EventManager_OnGlueSceneStarted(BrokenObject _brokenObject)
    {
        animator.SetTrigger("GlueSceneStarted");
    }

    private void EventManager_OnGlueSceneFinished(float score,BrokenObject _brokenObject)
    {
        animator.SetTrigger("GlueSceneFinished");

        HandleStarAnimationSelection(score);

        _scoreText.text = score.ToString();
    }

    private void HandleStarAnimationSelection(float score)
    {
        animator.SetFloat("Score", score);
    }

    public void LevelSelectorExitButtonClick()
    {
        EventManager.instance.OnLevelSelectorExitButtonClicked.Invoke(selectedObject,_selectedSpeechBubble);
        selectedObject = null;
        _selectedSpeechBubble = null;
    }

    private void EventManager_OnGlueParticleSpawned(GameObject _glueParticle, GlueController _glueController)
    {
        glueSlider.value += 1;
    }

    private void EventManager_OnGlueSceneInitialized(int _glueParticleCount)
    {
        glueSlider.maxValue = _glueParticleCount;
        glueSlider.value = 0;
    }

    private void EventManager_OnShapeRayInitialized(ShapeRay shapeRay)
    {
        _shapeRay = shapeRay;
    }
}

// Attach this script to a Camera
//Also attach a GameObject that has a Renderer (e.g. a cube) in the Display field
//Press the space key in Play mode to capture

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class Example : MonoBehaviour
{
    // Grab the camera's view when this variable is true.
    bool grab;
    bool isInInitialState = true;

    // The "m_Display" is the GameObject whose Texture will be set to the captured image.
    public Renderer m_Display;
    public Color initialAverageColor,endAverageColor = Color.clear;

    private void Awake()
    {
        EventManager.instance.OnGlueSceneInitialized.AddListener(EventManager_OnGlueSceneInitialized);
        EventManager.instance.OnGlueSceneLevelFinished.AddListener(EventManager_OnGlueSceneLevelFinished);
    }

    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
        if (grab)
        {
            //Create a new texture with the width and height of the screen
            Texture2D texture = new Texture2D(Screen.currentResolution.width, Screen.currentResolution.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, Screen.currentResolution.width, Screen.currentResolution.height), 0, 0, false);
            texture.Apply();
            var pixels = texture.GetPixels();
            Color averageColor = new Color(0, 0, 0);
            for (int i = 0; i < pixels.Length; i++)
            {
                averageColor.r += pixels[i].r;
                averageColor.g += pixels[i].g;
                averageColor.b += pixels[i].b;
            }
            averageColor = averageColor / pixels.Length;
            //Check that the display field has been assigned in the Inspector
            if (m_Display != null)
                //Give your GameObject with the renderer this texture
                m_Display.material.mainTexture = texture;
            //Reset the grab state
            grab = false;
            if (isInInitialState)
            {
                initialAverageColor = averageColor;
                isInInitialState = false;
            }
            else
            {
                endAverageColor = averageColor;
            }
        }
    }

    private void EventManager_OnGlueSceneInitialized(int x)
    {
        grab = true;
    }

    private void EventManager_OnGlueSceneLevelFinished()
    {
        grab = true;
    }

    IEnumerator WaitForEndAverageColor()
    {
        while(endAverageColor == Color.clear)
        {
            yield return null;
        }

        //Calculate score
        //EventManager.instance.OnGlueSceneFinished.Invoke(80);
    }
}
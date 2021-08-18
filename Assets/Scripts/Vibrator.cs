using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vibrator
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaClass vibrationEffect = new AndroidJavaClass("android.os.VibrationEffect");
    public static AndroidJavaObject currenctActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currenctActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

#endif
    public static void Vibrate(int effect)
    {
        if (IsAndroid())
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator.Call("vibrate",vibrationEffect.CallStatic<AndroidJavaObject>("createPredefined", effect));
#endif
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    public static bool IsAndroid()
    {
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

}

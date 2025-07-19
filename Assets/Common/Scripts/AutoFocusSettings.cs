/*========================================================================
Copyright (c) 2016-2017 PTC Inc. All Rights Reserved.

Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
using UnityEngine;
using Vuforia;

public class AutoFocusSettings : MonoBehaviour
{
#if (UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID)
    void Start()
    {
        var vuforia = VuforiaApplication.Instance;    
     VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
     VuforiaApplication.Instance.OnVuforiaPaused += OnPaused;
     
        //vuforia.RegisterOnPauseCallback(OnPaused);
    }  

    private void OnVuforiaStarted()
    {
        SetCameraFocusToAuto();
    }

    private void OnPaused(bool paused) {    
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            SetCameraFocusToAuto();
        }
    }

    private void SetCameraFocusToAuto()
    {
        Debug.Log("The focus of the device camera is set to 'Auto'");
        // CameraDevice.Instance.SetFocusMode(
        //     CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        // Note: CameraDevice.Instance is still available, but deprecated. 
        // It is still functional, but the enum needs to be adapted:
     //   CameraDevice.Instance.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        
        
    }
#endif
}
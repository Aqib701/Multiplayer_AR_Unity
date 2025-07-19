/*===============================================================================
Copyright (c) 2023 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;

public class DeviceObserverHelper : MonoBehaviour
{
    public void ResetDeviceObserver()
    {
        var devicePoseBehaviour = VuforiaBehaviour.Instance.DevicePoseBehaviour;
        if(!devicePoseBehaviour.enabled)
            return;
        if (devicePoseBehaviour.Reset())
            Debug.Log("Successfully reset Device Tracker");
        else
            Debug.LogError("Failed to reset Device Tracker");
    }
}

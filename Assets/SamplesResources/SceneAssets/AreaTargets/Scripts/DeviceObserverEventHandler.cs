/*===============================================================================
Copyright (c) 2023 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;

public class DeviceObserverEventHandler : MonoBehaviour
{
    public DeviceObserverResetPopup ResetPopup;

    DeviceObserverHelper mDeviceObserverHelper;
    bool mRelocalizing;
    float mRelocalizingTime;

    const float RELOCALIZING_THRESHOLD = 10;

    void Awake()
    {
        mDeviceObserverHelper = FindObjectOfType<DeviceObserverHelper>();
    }

    void Start()
    {
        VuforiaBehaviour.Instance.DevicePoseBehaviour.OnTargetStatusChanged += OnDevicePoseStatusChanged;
    }
    
    void Update()
    {
        if (!mRelocalizing) 
            return;
        
        mRelocalizingTime += Time.deltaTime;
        if (mRelocalizingTime >= RELOCALIZING_THRESHOLD)
        {
            mDeviceObserverHelper.ResetDeviceObserver();
            RelocalizationStopped();
        }
    }
    
    void OnDestroy()
    {
        if (VuforiaBehaviour.Instance != null)
            VuforiaBehaviour.Instance.DevicePoseBehaviour.OnTargetStatusChanged -= OnDevicePoseStatusChanged;
    }

    void OnDevicePoseStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.StatusInfo == StatusInfo.RELOCALIZING)
            RelocalizationStarted();
        else
            RelocalizationStopped();
    }

    void RelocalizationStarted()
    {
        mRelocalizing = true;
        mRelocalizingTime = 0;
        ResetPopup.Show(RELOCALIZING_THRESHOLD);
    }

    void RelocalizationStopped()
    {
        mRelocalizing = false;
        mRelocalizingTime = 0;
        if (ResetPopup != null)
            ResetPopup.Hide();
    }
}
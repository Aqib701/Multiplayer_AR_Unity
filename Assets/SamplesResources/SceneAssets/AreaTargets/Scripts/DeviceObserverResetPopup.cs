/*===============================================================================
Copyright (c) 2023 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class DeviceObserverResetPopup : MonoBehaviour
{
    const string RESET_TIMER_LABEL = "The Tracker will reset automatically in ";

    public Text TimerText;

    float mCountdownSeconds;
    float mElapsedTime;

    void Update()
    {
        mElapsedTime += Time.deltaTime;
        TimerText.text = $"{ RESET_TIMER_LABEL }{ Mathf.CeilToInt(mCountdownSeconds - mElapsedTime) }s";
        
        if (mElapsedTime >= mCountdownSeconds)
            gameObject.SetActive(false);
    }

    public void Show(float countdownSeconds)
    {
        mCountdownSeconds = countdownSeconds;
        mElapsedTime = 0;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


 // ** Controls Timer in a Game Session and Calculates who wins the Game **
public class PhotonTimer : MonoBehaviourPunCallbacks
{


    public static PhotonTimer Instance;
  
    [SerializeField] double timer = 20;
    [SerializeField] Text timerText;
    ExitGames.Client.Photon.Hashtable _customeValue;
    private double _timerIncrementValue;
    private double _startTime;
    bool _startTimer = false;
    private bool _initializeTime;

    private void Awake()
    {
        Instance = this;
    }

    public void StartCountingTime()
    {
       
        if (!_initializeTime)
        {
            Debug.Log("Called by master");
            _customeValue = new ExitGames.Client.Photon.Hashtable();
            _startTime = PhotonNetwork.Time;
            _customeValue.Add("StartTime", _startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(_customeValue);
            _initializeTime = true;


        }
        else
        {
            Debug.Log("Called by client");
            Debug.Log("Time: "+PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            _startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            
            if(!_startTimer)
                GameManager.Instance.SpawnACoinGroup();
            
            _startTimer = true;
            
            
          
        }


    }
 
    void Update()
    {
 
        if (!_startTimer) return;
 
        _timerIncrementValue = PhotonNetwork.Time - _startTime;


        _timerIncrementValue = timer-_timerIncrementValue;
        
        timerText.text = "Time Left : " + _timerIncrementValue.ToString("00") +"s";
        
        if (_timerIncrementValue <= 0)
        {

            if (PhotonNetwork.IsMasterClient)
            {
                GameManager.Instance.CalculateWin();
                _startTimer = false;
            }
            

        }
    }
    



    
    
}
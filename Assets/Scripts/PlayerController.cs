using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
   
// The Cube is Our Player, This script controlls the functionality and behaviour of player cube i.e Player Movements and Collection on Coins etx

public class PlayerController : MonoBehaviourPun {

    
    
   

    [SerializeField] float movementSpeed;
    [SerializeField] Text coinCollectedText;
    [SerializeField] GameObject joystickObject;
    
    
    [SerializeField] public TextMesh playerNameText;
   
    [HideInInspector] public  int numberofCoins=0;
    private GameObject _hopeTower;
    private Rigidbody _rb;
  

    void Start () {
 
        _rb = GetComponent<Rigidbody> ();
        SetPlayerColors();
       _hopeTower = GameObject.FindGameObjectWithTag("HopeTower");

       
    //   Debug.Log(_hopeTower.name);
       
    }


    private void Awake()
    {
        
        if (photonView.IsMine)
        {
            joystickObject.SetActive(true);

        }
        else
        {
            joystickObject.SetActive(false);
        }
        
    }



    public void UpdateNumberofCoins(int value)
    {

        numberofCoins += value;
        coinCollectedText.text = numberofCoins.ToString();

    }
    
    void FixedUpdate()
    {
        
        float x ,y;
        
        if (photonView.IsMine)
        {
             x = CrossPlatformInputManager.GetAxis("Horizontal");
             y = CrossPlatformInputManager.GetAxis("Vertical");
        }
        else
        {
             x = 0;
             y = 0;
        }
        

        Vector3 movement = new Vector3 (x, 0.0f, y)*(movementSpeed/6);



        // if (Input.GetKey("w"))
        // {
        //      movement = new Vector3 (1, 0.0f, 0)*(movementSpeed/6); 
        //     
        // }
        
        
        
             _rb.velocity = movement;


        if (x != 0 && y != 0) {
            transform.eulerAngles = new Vector3 (0, Mathf.Atan2 (x, y) * Mathf.Rad2Deg, 0)*(movementSpeed/3);
        }

    }



    public void SetPlayerNameText(string playerName)
    {
        
            playerNameText.text=playerName;

    }

    
    public void SetPlayerColors()
    {
        
        if (photonView.IsMine)
        {
            GetComponent<MeshRenderer>().material.color=Color.blue; // For player
            
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red; // For Opponents
        }

    }
    
    
    
}
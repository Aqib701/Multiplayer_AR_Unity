using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class CoinToss : MonoBehaviour
{
    public static bool isHead;
    public static bool isTails;
    public static bool ShowHelpText = true;
    public float UpForce;
    public float RotationTorque;
    private Rigidbody coinRB;
    public bool Canflip; // Also represents if the coin is in air or not, False means in Air
    public GameObject HelpText;
    public GameObject ResultText;
    void Start()
    {
        coinRB = GetComponent<Rigidbody>();
        HelpText.SetActive(ShowHelpText);
        ResultText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1.5f;
        
        
       // Debug.Log("Head:"+isHead);
       // Debug.Log("Tails:"+isTails);
        

        if (ShowHelpText)
        {
            ResultText.SetActive(false);
        }
        else if(Canflip)
        {

            if (isHead)
            {

                ResultText.GetComponent<TextMeshProUGUI>().SetText("Its Tail!");
                //Debug.Log("Heads");
            }
            else
            {
                ResultText.GetComponent<TextMeshProUGUI>().SetText("Its Head!");
               //Debug.Log("Tails");
            }
            
            
            ResultText.SetActive(true);
            
        }


        if (!Canflip)
        {

            ResultText.SetActive(false);
        }
        
            if (ShowHelpText == false && Canflip)
            {
                HelpText.SetActive(false);
            }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
           
                TossCoin();

        }
        
        
        
       // Debug.Log("Canflip:"+Canflip);

       

    }


    
    



    public void TossCoin()
    {
        if (Canflip)
        {

            UpForce = UnityEngine.Random.Range(7.5f, 13f);
            coinRB.AddForce(new Vector3(0, UpForce, 0), ForceMode.Impulse);
            coinRB.AddTorque(new Vector3(0, 0, RotationTorque) * 100, ForceMode.Impulse);
            ShowHelpText = false;
            ResultText.GetComponent<TextMeshProUGUI>().SetText("");
        }
    }


    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Canflip = false;
        }
        
    }
    
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Canflip = true;
        }
        
    }
    
    
    
    
    
}

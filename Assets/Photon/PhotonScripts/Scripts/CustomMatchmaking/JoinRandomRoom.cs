using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class JoinRandomRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }






    public void JoinAnyRoom()
    {


        if (PhotonNetwork.JoinRandomRoom())
        {
            
            Debug.Log("Random Room Joined!");

        }
        else
        {
            Debug.Log("No Room Found");
        }


    }
    
    
    
    
    
    
}

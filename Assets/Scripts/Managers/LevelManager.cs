using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    // To change between scenes


    public void ReturnToMenu()
    {
        
        
        
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
        
    }
    
    
    
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
        
    }
    
}

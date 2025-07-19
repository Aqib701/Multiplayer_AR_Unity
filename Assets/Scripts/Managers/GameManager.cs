using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{



    public static GameManager Instance;
    
    
    
    
    [SerializeField] 
    private GameObject levelObject;
    [SerializeField] 
    private GameObject gameOverPanel;
    [SerializeField] 
    private Text winnerText;
    [SerializeField]
    private CoinsGroup[] coinsGroupsToSpawn;
    [SerializeField]
    private Transform[] coinGroupLocations;

    
    private bool _isGameOver;
    
    
    void Start()
    {
        Instance = this;
    }

    
    public void CalculateWin()   // Calculated who is the winner for our game
    {

        if (_isGameOver) return;
        
        
        
        string winnerName;
        int winnerCoinNumbers;
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        winnerName = players[0].GetComponent<PlayerController>().playerNameText.text;
        winnerCoinNumbers = players[0].GetComponent<PlayerController>().numberofCoins;


        foreach (var player in players)
        {
            if (player.GetComponent<PlayerController>().numberofCoins > winnerCoinNumbers)
            {


                winnerName = player.GetComponent<NameTag>().nameText.text;
                winnerCoinNumbers = player.GetComponent<PlayerController>().numberofCoins;

            }
            
            winnerText.text = "Winner is '" + winnerName + "' ! ";
        }


        _isGameOver = true;
        gameOverPanel.SetActive(true);
        
        PhotonNetwork.Disconnect();
        
        

    }
    
    
    public void SpawnACoinGroup()
    {
        
        
        if (!PhotonNetwork.IsMasterClient) return; // Do nothing if called by other clients
        
        Debug.Log("Spawn Coin Group");
        
        int coinGroupIndex = Random.Range(0, coinsGroupsToSpawn.Length);
        int coinGroupLocationsIndex = Random.Range(0, coinGroupLocations.Length);


        GameObject coin= PhotonNetwork.Instantiate(coinsGroupsToSpawn[coinGroupIndex].name, 
            coinGroupLocations[coinGroupLocationsIndex].position, Quaternion.identity, 0);

        coin.transform.parent = levelObject.transform;

    }
    
    
}


using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{

    
    // This script Controls Functionality related to online photon Room making and Joining
    
    
    public static CustomMatchmakingRoomController Instance;


    [SerializeField]
    private GameObject levelObject;
    [SerializeField]
    private GameObject lobbyPanel; //display for when in lobby
    [SerializeField]
    private GameObject roomPanel; //display for when in room
    [SerializeField]
    private GameObject startButton; //only for the master client. used to start the game and load the multiplayer scene
    [SerializeField]
    private Transform playersContainer; //used to display all the players in the current room
    [SerializeField]
    private GameObject playerListingPrefab; //Instantiate to display each player in the room

    [SerializeField]
    private Text roomNameDisplay; //display for the name of the room
    [SerializeField]
    private Text connectionText;

    [SerializeField] 
    private float gameStartTime;
    [SerializeField]
    private GameObject[] playerModels;
    
    [SerializeField]
    private Transform[] spawnPoints;
    
    
    private float _timer = 0;
    private bool _playerSpawned;
    

    

    private void Start()
    {
        Instance = this;
      //  photonTimer.enabled = false;
        _timer = gameStartTime;
        connectionText.text = "Connecting to Main Server...";
    }

    public override void OnDisconnected(DisconnectCause cause) {
        connectionText.text = cause.ToString();
    }
    
    public override void OnJoinedLobby() {
        connectionText.text = "";
    }
    
    public void JoinAnyRoom()
    {
        
        PhotonNetwork.JoinRandomRoom(); // Join any room you can find
        
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        
        connectionText.text = "Failed to Find Random Room Creating New Room!";
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)2 };
        PhotonNetwork.CreateRoom(null, roomOps); //attempting to create a new room
    }
    
    void ClearPlayerListings()
    {
        for (int i = playersContainer.childCount - 1; i >= 0; i--) //loop through all child object of the playersContainer, removing each child
        {
            Destroy(playersContainer.GetChild(i).gameObject);
        }
    }

    void ListPlayers() 
    {

        foreach (Player player in PhotonNetwork.PlayerList) //loop through each player and create a player listing
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playersContainer);
            Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
            tempText.text = player.NickName;
        }
        
    }

    public override void OnJoinedRoom()                  //called when the local player joins the room
    {
        
        connectionText.text = "Joined room !";
        
        if (!PhotonNetwork.IsConnectedAndReady) 
        {
            connectionText.text = "PhotonNetwork connection is not ready, try restart it.";
        }
      
        
        Debug.Log("Number of Players:"+PhotonNetwork.CurrentRoom.PlayerCount.ToString());
        
        
        roomPanel.SetActive(true); //activate the display for being in a room
        lobbyPanel.SetActive(false); //hide the display for being in a lobby
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name; //update room name display
        if (PhotonNetwork.IsMasterClient) //if master client then activate the start button
        {
            startButton.SetActive(true);
        }

        ClearPlayerListings(); //remove all old player listings
        ListPlayers(); //relist all current player listings
    }

    
    public override void OnPlayerEnteredRoom(Player newPlayer) //called whenever a new player enter the room
    {
        
        Debug.Log("Number of Players:"+PhotonNetwork.CurrentRoom.PlayerCount.ToString());
        ClearPlayerListings(); //remove all old player listings
        
        ListPlayers(); //relist all current player listings
        
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)//called whenever a player leave the room
    {
        ClearPlayerListings();//remove all old player listings
        
        ListPlayers();//relist all current player listings
        if (PhotonNetwork.IsMasterClient)//if the local player is now the new master client then we activate the start button
        {
            startButton.SetActive(true);
        }
    }

    public void StartGameOnClick()    //paired to the start button. will load all players into the multiplayer scene through the master client and AutomaticallySyncScene
    {
        
        //  if(PhotonNetwork.IsMasterClient)
        { 
            PhotonNetwork.CurrentRoom.IsOpen = true;            //Comment out if you want player to join after the game has started
            
           connectionText.text = "Joined!";
           _playerSpawned = true;
           // Cursor.lockState = CursorLockMode.Locked;
           Cursor.visible = false;
           Respawn(0.0f);
           roomPanel.SetActive(false);


        }
    }
 
    
    public void Respawn(float spawnTime) {
       
     //   sceneCamera.SetActive(true);
        StartCoroutine(RespawnCoroutine(spawnTime));
     
    }
    

    [PunRPC]
    void respawn_RPC()
    {
        // Now we have Opponent joined and we can start the Timer and Spawn Coins
        
            var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            
        
            PhotonTimer.Instance.StartCountingTime();
            
            // // Debug.Log("PlayerCount: "+ playerCount);
            //
            //    if (playerCount <= 1)
            //    {
            //        PhotonTimer.Instance.StartCountingTime(false); // Initialize Timer Here for sync
            //    }
            //    else
            //    {
            //       
            //        PhotonTimer.Instance.StartCountingTime(true); // Actually Start Timer here
            //        GameManager.Instance.SpawnACoinGroup();
            //    }
    }
    
    
    
    IEnumerator RespawnCoroutine(float spawnTime) {
        
        yield return new WaitForSeconds(spawnTime);
        
        int playerIndex = Random.Range(0, playerModels.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        
       var player = PhotonNetwork.Instantiate(playerModels[playerIndex].name, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation, 0);
        player.transform.parent = levelObject.transform;
        
        player.GetComponent<PlayerController>().SetPlayerNameText(PhotonNetwork.LocalPlayer.NickName);
        photonView.RPC("respawn_RPC",RpcTarget.All);

    }
    
    
    
    
    
    
    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick() // paired to the back button in the room panel. will return the player to the lobby panel.
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }
    
    IEnumerator StartGameInSeconds(float time)
    {
        _playerSpawned = true;
        yield return new WaitForSeconds(time);
       // StartGameOnClick();
       startButton.SetActive(true);
        
    }
    
    
    public override void OnConnectedToMaster()
    {
        connectionText.text = "";
    }
    
    
    
    
    
    
}

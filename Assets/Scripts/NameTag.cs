using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameTag : MonoBehaviourPunCallbacks {

    
    // ** Handles Names on Top of each Character **
    
    [HideInInspector]
    public Transform target = null;

    [SerializeField]
    public TextMesh nameText;

   
    void Start() {
        
        if (photonView.IsMine) {
            
            photonView.RPC("SetName", RpcTarget.All, PhotonNetwork.NickName); // Remote Procedural Calls
            
        } else {
            
            SetName(photonView.Owner.NickName);
        }
    }

  
   
    [PunRPC]
    void SetName(string name) {
        nameText.text = name;
    }
    

}

using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// ** Synchronizes Player movements in a Server ** 

public class PlayerNetworkMover :MonoBehaviourPunCallbacks,IPunObservable {

    private Vector3 _position;
    private Vector3 _scale;
    private Quaternion _rotation;
    private bool _jump;
    private float smoothing = 10.0f;
  //  private GameObject _spawnPoint;


    private void Start()
    {
      //  _spawnPoint=GameObject.Find("PlayerSpawnPoint");

    }

    void Update() {
        
        
        if (!photonView.IsMine)
        {
            Vector3 cubepos = _position;
            transform.position = Vector3.Lerp(transform.position, cubepos, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, Time.deltaTime * smoothing);
          //  transform.localScale = _scale;
        }
    }

  
    /// Used to customize synchronization of variables in a script watched by a photon network view.
    /// </summary>
    /// <param name="stream">The network bit stream.</param>
    /// <param name="info">The network message information.</param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting)
        {

            stream.SendNext(transform.position);//-_spawnPoint.transform.position);
            stream.SendNext(transform.rotation); 
            
           // stream.SendNext(transform.localScale);
        } else
        {
            _position = (Vector3) stream.ReceiveNext();//+_spawnPoint.transform.position;
            _rotation = (Quaternion)stream.ReceiveNext();
          //  _scale = (Vector3) stream.ReceiveNext();
        }
    }

}
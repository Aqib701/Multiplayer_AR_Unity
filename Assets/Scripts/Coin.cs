using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Coin which is collected by the players During Gameplay

public class Coin : MonoBehaviour
{


   public AudioClip pickupSound;
   private float _speed=100;
   private GameObject _hopetower;
   private void Start()
   {
      _hopetower=GameObject.Find("HopeTower");
     // Debug.Log("HopeTower:"+_hopetower.name);
   }


   private void Update()
   {
     // gameObject.GetComponent<MeshRenderer>().enabled = hopetower.GetComponent<MeshRenderer>().enabled;
     transform.Rotate(Vector3.up * _speed * Time.deltaTime);
   }
   
 


   public void OnTriggerEnter(Collider other)
   {
      
      if (other.gameObject.tag.Equals("Player"))
      {
         
         transform.parent.GetComponent<CoinsGroup>().UpdateCoinCount(-1);
         other.GetComponent<PlayerController>().UpdateNumberofCoins(+1);
         SoundManager.PlaySound(pickupSound);
         Destroy(this.gameObject);
         
      }
   }
}

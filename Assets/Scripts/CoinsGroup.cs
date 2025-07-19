using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinsGroup : MonoBehaviour
{
   public Coin[] coins;

  [HideInInspector] public int totalCoinsInGroup;
   

   private void OnEnable()
   {

      totalCoinsInGroup = coins.Length;
   }




   public void UpdateCoinCount(int count)
   {

      totalCoinsInGroup += count;

   }
   
   
   private void Update()
   {

      if (totalCoinsInGroup <= 0)
      {
         
         
         GameManager.Instance.SpawnACoinGroup();
         Destroy(this.gameObject);

      }
      
      
   }
}

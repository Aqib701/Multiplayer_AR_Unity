using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
            CoinToss.isTails = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        CoinToss.isTails = false;
    }
   
}

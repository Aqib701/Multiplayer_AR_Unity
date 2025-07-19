using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
            CoinToss.isHead = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        CoinToss.isHead = false;
    }
}

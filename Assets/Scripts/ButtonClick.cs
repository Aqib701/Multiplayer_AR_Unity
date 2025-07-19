using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{

    public AudioClip OnClickClip;
    private Button button;
    
    
    void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(PlayClickSound);
        
    }



    void PlayClickSound()
    {
        
        
        SoundManager.PlaySound(OnClickClip);
        
    }
    
    
}

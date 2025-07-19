using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    public SoundManager soundManager;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public Image SpriteImage;
  
    // Update is called once per frame
    void Update()
    {
        if (soundManager.isMute)
        {
            SpriteImage.sprite = SoundOff;
        }
        else
        {
            SpriteImage.sprite = SoundOn;
        }
    }
}

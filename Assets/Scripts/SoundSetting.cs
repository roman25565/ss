using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    private bool _enabled = true;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image image;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;

    public void OnClick()
    {
        if (_enabled)
        {
            _enabled = false;
            audioSource.volume = 0;
            image.sprite = spriteOff;
        }
        else
        {
            _enabled = true;
            audioSource.volume = 100;
            image.sprite = spriteOn;
        }
    }
    
}

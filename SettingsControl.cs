using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    public GameObject AudioVolumeSlider;

    public void Start()
    {
        AudioVolumeSlider.GetComponent<Slider>().value = (float)AudioManager.Instance.Audio.volume;
    }

    public void ChangeVolume()
    {
        AudioManager.Instance.SetVolume((float)AudioVolumeSlider.GetComponent<Slider>().value);
    }
}

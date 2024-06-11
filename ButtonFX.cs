using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioClip MOverSound;
    public AudioClip MClickSound;

    public void PlayMOverSound()
    {
        AudioManager.Instance.PlaySound(MOverSound);
    }

    public void PlayMClickSound()
    {
        AudioManager.Instance.PlaySound(MClickSound);
    }
}

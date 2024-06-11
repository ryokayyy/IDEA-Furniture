using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public AudioSource Audio;

    // Start is called before the first frame update
    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        Audio.PlayOneShot(clip);
    }

    public void SetVolume(float value)
    {
        Audio.volume = value;
    }
}

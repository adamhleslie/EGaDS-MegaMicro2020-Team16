using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundAsset
{
    public string soundName;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1;
    [Range(.1f, 1)] public float minPitch = 1;
    [Range(1, 3)] public float maxPitch = 1;
    [HideInInspector] public AudioSource source;
}

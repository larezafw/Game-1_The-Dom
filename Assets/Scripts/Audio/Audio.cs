using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio 
{
    public string name;

    public AudioClip clips;

    [Range(0f,1f)]
    public float volume;

    [Range(-1,3f)]
    public float pitch;

    public bool loop;
    [HideInInspector]
    public AudioSource sources;
}

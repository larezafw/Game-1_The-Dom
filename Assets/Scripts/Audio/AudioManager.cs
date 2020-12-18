using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] sound;

    void Awake()
    {
        foreach(Audio a in sound)
        {
           a.sources= gameObject.AddComponent<AudioSource>();
            a.sources.clip = a.clips;

            a.sources.volume = a.volume;
            a.sources.pitch = a.pitch;
            a.sources.loop = a.loop;
        }
    }

    public void Play( string name)
    {
       Audio s= Array.Find(sound, sound => sound.name == name);
       s.sources.Play();
    }

    public void Stop(string name)
    {
        Audio s = Array.Find(sound, sound => sound.name == name);
        s.sources.Stop();
    }
}

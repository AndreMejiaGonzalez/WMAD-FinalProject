using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;

    private void Update() {
        source.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    public void playClip(int clipIndex)
    {
        source.PlayOneShot(clips[clipIndex]);
    }

    public void playClipLooping()
    {
        source.loop = true;
        source.PlayDelayed(1);
    }
}

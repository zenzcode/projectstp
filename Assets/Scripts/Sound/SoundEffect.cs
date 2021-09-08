using System;
using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    public AudioClip audioClip;
    public SoundEffectType soundEffectType;
    [Range(0, 1)]
    public float volume;
    [Range(-3, 3)]
    public float randomPitchMinValue;
    [Range(-3, 3)]
    public float randomPitchMaxValue;
}
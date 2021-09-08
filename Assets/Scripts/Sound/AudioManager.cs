using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private SO_SoundEffects so_soundEffects;

    [SerializeField]
    private GameObject soundPrefab;

    private Dictionary<string, SoundEffect> _audioClips;

    private void Awake()
    {
        _audioClips = new Dictionary<string, SoundEffect>();
        InitialiseSoundEffects();
    }

    public void PlaySound(SoundEffectType soundEffectType)
    {
        if(_audioClips.TryGetValue(soundEffectType.ToString(), out var audioClip))
        {
            var currentObject = PoolManager.Instance.ReuseGameObject(soundPrefab, Vector3.zero, Quaternion.identity);
            if (currentObject == null) return;
            var audioComponent = currentObject.GetComponent<AudioSource>();
            audioComponent.clip = audioClip.audioClip;
            audioComponent.volume = audioClip.volume;
            audioComponent.pitch = Random.Range(audioClip.randomPitchMinValue, audioClip.randomPitchMaxValue);
            StartCoroutine(DisableObject(currentObject, audioClip.audioClip.length));
        }
    }

    private IEnumerator DisableObject(GameObject soundObject, float clipLength) {
        yield return new WaitForSeconds(clipLength);
        soundObject.SetActive(false);
    }

    private void InitialiseSoundEffects()
    {
        foreach(var soundEffect in so_soundEffects.soundEffects)
        {
            _audioClips.Add(soundEffect.soundEffectType.ToString(), soundEffect);
        }
    }
}

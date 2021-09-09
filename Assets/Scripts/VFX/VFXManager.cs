using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : SingletonMonoBehaviour<VFXManager>
{

    [SerializeField]
    private GameObject damagePrefab;
    [SerializeField]
    private GameObject jumpStartPrefab;
    [SerializeField]
    private GameObject jumpEndPrefab;

    private void OnEnable()
    {
        EventHandler.EffectSpawnEvent += OnEffectSpawn;
    }

    private void OnDisable()
    {
        EventHandler.EffectSpawnEvent -= OnEffectSpawn;
    }

    private void OnEffectSpawn(VisualEffectType visualEffectType, Vector3 position)
    {   
        switch (visualEffectType)
        {
            case VisualEffectType.Damage:
                HandleEffectSpawn(damagePrefab, position);
                break;
            case VisualEffectType.JumpStart:
                HandleEffectSpawn(jumpStartPrefab, position);
                break;
            case VisualEffectType.JumpLand:
                HandleEffectSpawn(jumpEndPrefab, position);
                break;
        }
    }

    private void HandleEffectSpawn(GameObject prefab, Vector3 position)
    {
        var effectObject = PoolManager.Instance.ReuseGameObject(prefab, position, Quaternion.identity);
        var particleSystem = effectObject.GetComponent<ParticleSystem>();
        if (particleSystem == null) return;
        effectObject.SetActive(true);
        particleSystem.Play();
        StartCoroutine(DeactivateParticles(effectObject, particleSystem.main.duration));
    }

    private IEnumerator DeactivateParticles(GameObject effectObject, float duration)
    {
        yield return new WaitForSeconds(duration);
        effectObject.SetActive(false);
    }
}

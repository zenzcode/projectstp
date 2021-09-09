using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float minShake;
    [SerializeField] private float maxShake;
    [SerializeField] private float magnitude;

    private Vector3 _originalPos;

    private Coroutine _cameraShakeRoutine = null;

    private void OnEnable()
    {
        EventHandler.CameraShakeEvent += ShakeCamera;
    }

    private void OnDisable()
    {
        EventHandler.CameraShakeEvent -= ShakeCamera;
    }
    private void ShakeCamera(float time)
    {
        if (_cameraShakeRoutine != null) return;
        _cameraShakeRoutine = StartCoroutine(Shake(time));
    }

    private IEnumerator Shake(float shakeTime)
    {
        _originalPos = transform.localPosition;
        var elapsedTime = 0f;
        while (elapsedTime < shakeTime)
        {
            var xShake = Random.Range(minShake, maxShake) * magnitude;
            var yShake = Random.Range(minShake, maxShake) * magnitude;

            transform.localPosition = new Vector3(xShake, yShake, _originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _originalPos;
        StopCoroutine(_cameraShakeRoutine);
        _cameraShakeRoutine = null;
    }
}

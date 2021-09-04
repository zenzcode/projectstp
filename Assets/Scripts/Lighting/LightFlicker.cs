using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Light2D))]
public class LightFlicker : MonoBehaviour
{
    private bool _shouldFlicker;
    [SerializeField] private float lightFlickerMinTime;
    [SerializeField] private float lightFlickerMaxTime;

    [SerializeField]
    [Range(0f, 1f)]
    private float lightFlickerIntensity;

    private Light2D _light;
    private float _currentIntensity;
    private float _lightFlicker;

    public bool ShouldFlicker
    {
        get => _shouldFlicker;
        set => _shouldFlicker = value;
    }

    private void Awake()
    {
        _light = GetComponent<Light2D>();

        if (_light)
            _shouldFlicker = true;
        
        ResetLightFlicker();
        _currentIntensity = _light.intensity;
    }

    private void Update()
    {
        if (!_shouldFlicker) return;
        _lightFlicker -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        HandleFlickering();
    }

    private void HandleFlickering()
    {
        if (_lightFlicker <= 0 && ShouldFlicker)
        {
            ResetLightFlicker();

            _light.intensity = Random.Range(_currentIntensity,
                _currentIntensity + (_currentIntensity * lightFlickerIntensity));
        }
    }

    private void ResetLightFlicker()
    {
        _lightFlicker = Random.Range(lightFlickerMinTime, lightFlickerMaxTime);
    }
}

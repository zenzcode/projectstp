using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightControl : MonoBehaviour
{
    private Light2D _light;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    public void DisableLight()
    {
        _light.intensity = 0;
    }

    public void EnableLight(float intensity, float fade)
    {
        if (fade == 0)
        {
            _light.intensity = intensity;
        }
        else
        {
            StartCoroutine(FadeLight(intensity, fade));
        }
    }

    private IEnumerator FadeLight(float intensity, float fade)
    {
        yield return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DebugLight : MonoBehaviour
{
    private Light2D _lightToDebug;
    private void Start()
    {
        _lightToDebug = GetComponentInChildren<Light2D>();
        if(_lightToDebug == null)
            Destroy(gameObject);

        if (Settings.IsDevMode)
            _lightToDebug.enabled = true;
    }
}

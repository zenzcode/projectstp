using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class TimedLightActivator : MonoBehaviour
{
    [SerializeField]
    private float activatedTime = 1;
    private float _timerActive;

    private Light2D _platformLight;

    private void Update()
    {
        
    }
}

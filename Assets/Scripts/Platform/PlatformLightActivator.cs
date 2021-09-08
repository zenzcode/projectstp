using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlatformLightActivator : MonoBehaviour
{
    private Light2D _lightToActivate;

    private void Awake()
    {
        _lightToActivate = GetComponentInChildren<Light2D>();
        if (_lightToActivate == null)
            Destroy(this);

        _lightToActivate.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            AudioManager.Instance.PlaySound(SoundEffectType.LightTurnOn);
            _lightToActivate.enabled = true;
            Destroy(this);
        }
    }
}

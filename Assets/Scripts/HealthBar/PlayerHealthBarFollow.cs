using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

    private GameObject _player;
    private Camera _mainCam;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(Tags.Player);
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        SetHealthbarPosition();
    }

    private void SetHealthbarPosition()
    {
        var screenPosPlayerWithOffset = _mainCam.WorldToScreenPoint(_player.transform.position + offset);

        transform.position = screenPosPlayerWithOffset;
        transform.localScale = new Vector3(GetCamScale(), GetCamScale(), GetCamScale());
    }

    private float GetCamScale()
    {
        return Settings.ZoomedOutCamSize / _mainCam.orthographicSize;
    }
}

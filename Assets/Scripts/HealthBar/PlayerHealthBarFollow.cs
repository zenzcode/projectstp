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
        var newPosition = Vector3.zero;
        var screenPosPlayer = _mainCam.WorldToScreenPoint(_player.transform.position);
        newPosition = screenPosPlayer + offset;

        transform.position = newPosition;
    }
}

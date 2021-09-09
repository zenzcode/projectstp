using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    private Coroutine _zoomOutRoutine;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        EventHandler.CameraZoomEvent += ZoomCamera;
    }

    private void OnDisable()
    {
        EventHandler.CameraZoomEvent -= ZoomCamera;
    }

    private void ZoomCamera(float target)
    {
        Debug.Log(target);
        if (_zoomOutRoutine != null) return;
        Debug.Log($"Started for target {target}");
        _zoomOutRoutine = StartCoroutine(ZoomCameraRoutine(target));
        
    }

    private IEnumerator ZoomCameraRoutine(float target)
    {
        while(!Mathf.Approximately(_mainCamera.orthographicSize, target))
        {
            if(_mainCamera.orthographicSize > target)
            {
                yield return null;
                _mainCamera.orthographicSize -= 0.01f;
            }
            else
            {
                yield return null;
                _mainCamera.orthographicSize += 0.01f;
            }
        }
        _mainCamera.orthographicSize = target;
        StopCoroutine(_zoomOutRoutine);
        _zoomOutRoutine = null;
        Debug.Log($"Finished for target {target}");
        Debug.Log($"{_zoomOutRoutine}");
        
    }
}

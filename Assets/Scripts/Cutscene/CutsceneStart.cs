using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneStart : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    [SerializeField] private Transform cutscenePoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            other.transform.position = cutscenePoint.transform.position;
            EventHandler.CallCameraZoomEvent(8);
            Player.Instance.SetCanMove(false);
            playableDirector.Play();
        }
    }
}

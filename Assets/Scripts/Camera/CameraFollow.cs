using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float delay;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}

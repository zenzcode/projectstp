using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColoredByMemories : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Color determinedColor;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.color = startColor;
    }

    private void GetColorDifference()
    {
        //TODO: Fill
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColoredByMemories : MonoBehaviour
{
    [SerializeField] private Color startColor;
    [SerializeField] private Color destinationColor;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetColor(startColor);
    }

    private void OnEnable()
    {
        EventHandler.MemoryCollectedEvent += UpdateColor;
    }

    private void OnDisable()
    {
        EventHandler.MemoryCollectedEvent += UpdateColor;
    }

    private void UpdateColor(int memoryId)
    {
        SetColor(GetColorDifference());
    }

    private void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    private Color GetColorDifference()
    {
        //TODO: Clamp values to values from destination
        if(destinationColor.r > destinationColor.g && destinationColor.r > destinationColor.b)
        {
            return new Color(destinationColor.r, startColor.g / MemoryManager.Instance.catchedMemories, startColor.b / MemoryManager.Instance.catchedMemories);
        }else if(destinationColor.g > destinationColor.r && destinationColor.g > destinationColor.b)
        {
            return new Color(startColor.r / MemoryManager.Instance.catchedMemories, destinationColor.g, startColor.b / MemoryManager.Instance.catchedMemories);
        }
        else
        {
            return new Color(startColor.g / MemoryManager.Instance.catchedMemories, startColor.g / MemoryManager.Instance.catchedMemories, destinationColor.b);
        }
    }
}

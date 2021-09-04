using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MemoryChest : MonoBehaviour
{
    [SerializeField] public Sprite memoryImage;
    private Animator _animator;
    private SpriteRenderer _childSpriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _childSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Player))
        {
            if (_childSpriteRenderer == null) return;
            _childSpriteRenderer.sprite = memoryImage;
            _animator.SetTrigger(Settings.PickupMemoryAnimation);
        }
    }
}

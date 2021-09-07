using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MemoryChest : MonoBehaviour
{
    public int memoryId;
    private Sprite _memoryImage;
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
            _childSpriteRenderer.sprite = _memoryImage;
            _animator.SetTrigger(Settings.PickupMemoryAnimation);
            MemoryManager.Instance.SelectMemory(memoryId);
        }
    }

    public void ReactivateMovememt()
    {
        Player.Instance.SetCanMove(true);
        Destroy(gameObject);
    }

    public void SetMemoryImage(Sprite memoryImage)
    {
        _memoryImage = memoryImage;
    }
}

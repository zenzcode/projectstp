using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : SingletonMonoBehaviour<MemoryManager>
{
    [SerializeField] private SO_MemoryList _soMemoryList;
    public int catchedMemories;

    public void SelectMemory(int memoryId)
    {
        Player.Instance.ResetMovementVelocity();
        Player.Instance.SetCanMove(false);
        catchedMemories++;
        EventHandler.CallMemoryCollectedEvent(memoryId);
    }
}

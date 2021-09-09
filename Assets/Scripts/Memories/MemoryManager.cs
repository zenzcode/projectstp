using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : SingletonMonoBehaviour<MemoryManager>
{
    public int catchedMemories;

    public void SelectMemory(int memoryId)
    {
        Player.Instance.ResetMovementVelocity();
        Player.Instance.SetCanMove(false);
        catchedMemories++;
        EventHandler.CallMemoryCollectedEvent(memoryId);
    }
}

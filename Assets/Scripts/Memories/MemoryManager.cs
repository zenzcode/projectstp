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
        Player.Instance.ResetJump();
        catchedMemories++;
        EventHandler.CallMemoryCollectedEvent(memoryId);
    }

    public int GetSelectedMemoryCount()
    {
        return catchedMemories;
    }

    public int GetMaxMemories()
    {
        return GameObject.FindGameObjectsWithTag(Tags.MemoryChest).Length;
    }
}

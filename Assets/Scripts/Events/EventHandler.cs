using System;

public class EventHandler
{
    public static event Action<int> MemoryCollectedEvent;

    public static void CallMemoryCollectedEvent(int memoryId)
    {
        MemoryCollectedEvent?.Invoke(memoryId);
    }
}
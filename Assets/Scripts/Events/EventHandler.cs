using System;

public class EventHandler
{
    public static event Action<int> MemoryCollectedEvent;

    public static void CallMemoryCollectedEvent(int memoryId)
    {
        MemoryCollectedEvent?.Invoke(memoryId);
    }

    public static event Action PlayerDeathEvent;

    public static void CallPlayerDeathEvent()
    {
        PlayerDeathEvent?.Invoke();
    }

    public static event Action OnDialogStarted;

    public static void CallDialogStartedEvent()
    {
        OnDialogStarted?.Invoke();
    }

    public static event Action OnDialogContinued;

    public static void CallDialogContinued()
    {
        OnDialogContinued?.Invoke();
    }

    public static event Action OnDialogEnded;
    
    public static void CallDialogEnded()
    {
        OnDialogEnded?.Invoke();
    }
}
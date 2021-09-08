using System;
using UnityEngine;

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

    public static event Action PlayerDamagedEvent;

    public static void CallPlayerDamagedEvent()
    {
        PlayerDamagedEvent?.Invoke();
    }

    public static event Action<VisualEffectType, Vector3> EffectSpawnEvent;

    public static void CallEffectSpawnEvent(VisualEffectType visualEffectType, Vector3 position)
    {
        EffectSpawnEvent?.Invoke(visualEffectType, position);
    }
}
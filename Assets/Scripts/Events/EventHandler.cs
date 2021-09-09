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

    public static event Action<float> PlayerDamagedEvent;

    public static void CallPlayerDamagedEvent(float newHealth)
    {
        PlayerDamagedEvent?.Invoke(newHealth);
    }

    public static event Action<VisualEffectType, Vector3> EffectSpawnEvent;

    public static void CallEffectSpawnEvent(VisualEffectType visualEffectType, Vector3 position)
    {
        EffectSpawnEvent?.Invoke(visualEffectType, position);
    }

    public static event Action<Checkpoint> CheckpointUpdatedEvent;

    public static void CallCheckpointUpdatedEvent(Checkpoint checkpoint)
    {
        CheckpointUpdatedEvent?.Invoke(checkpoint);
    }

    public static event Action<float> CameraZoomEvent;

    public static void CallCameraZoomEvent(float target)
    {
        CameraZoomEvent?.Invoke(target);
    }

    public static event Action<float> CameraShakeEvent;

    public static void CallCameraShakeEvent(float time)
    {
        CameraShakeEvent?.Invoke(time);
    }
}
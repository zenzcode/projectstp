using UnityEngine;

public static class Settings
{
    public const int MaxMemories = 10;
    public const int LowestObjectY = -22;

    public const float AttackBuildupSeconds = 1.25f;
    public const float AttackStaySeconds = 0.5f;
    public const float AttackTeardownSeconds = 0.75f;

    public static readonly int PickupMemoryAnimation = Animator.StringToHash("PickUp");
}
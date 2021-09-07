using UnityEngine;

public static class Settings
{
    public const int LowestObjectY = -22;

    public const float MaxInvincibleTime = 1f;
    public const float MaxAttackDelay = 0.69f;

    public static readonly int PickupMemoryAnimation = Animator.StringToHash("PickUp");
    public static readonly int PlayerAttackAnimation = Animator.StringToHash("Attacking");
}
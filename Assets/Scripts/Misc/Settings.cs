using UnityEngine;

public static class Settings
{
    public const bool IsDevMode = true;

    public const float MaxInvincibleTime = 1f;
    public const float MaxAttackDelay = 0.69f;

    public const float CamSize = 2;
    public const float ZoomedOutCamSize = 3;

    public const float MaxHealth = 100f;

    public const float SecondsUntilDead = 2.5f;

    public static readonly int PickupMemoryAnimation = Animator.StringToHash("PickUp");
    public static readonly int PlayerAttackAnimation = Animator.StringToHash("Attacking");
    public static readonly int PlayerInjuredAnimation = Animator.StringToHash("Injured");
    public static readonly int PlayerInjuredEndAnimation = Animator.StringToHash("InjuredEnd");
}
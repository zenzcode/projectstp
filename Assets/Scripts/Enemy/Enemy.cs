using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            if (Player.Instance.IsInvincible) return;
            var healthScript = collision.gameObject.GetComponent<Health>();
            if (healthScript == null) healthScript = collision.gameObject.GetComponentInParent<Health>();
            if (healthScript == null) healthScript = collision.gameObject.GetComponentInChildren<Health>();
            if (healthScript == null) {
                Debug.LogError("HealthScript on Object not Found");
                return;
            }
            
            AudioManager.Instance.PlaySound(SoundEffectType.Damage);
            EventHandler.CallEffectSpawnEvent(VisualEffectType.Damage, Player.Instance.transform.position);
            Player.Instance.ResetYVelocity();
            healthScript.TakeDamage(damage);
            EventHandler.CallPlayerDamagedEvent();
        }
    }
}

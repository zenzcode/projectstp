using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    private void OnEnable()
    {
        EventHandler.PlayerDamagedEvent += TakeDamage;
    }

    private void OnDisable()
    {
        EventHandler.PlayerDamagedEvent -= TakeDamage;
    }

    public void TakeDamage(float newHealth)
    {
        health = Mathf.Max(0, newHealth);

        if(health == 0)
        {
            EventHandler.CallPlayerDeathEvent();
        }
    }   

    public float GetHealth()
    {
        return health;
    }

}

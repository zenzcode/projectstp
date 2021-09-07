using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;

    private bool _isPlayer;
     
    private void Awake()
    {
        _isPlayer = GetComponent<Player>() != null || 
            GetComponentInParent<Player>() != null || 
            GetComponentInChildren<Player>() != null;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);

        if(health == 0)
        {
            if (_isPlayer)
            {
                EventHandler.CallPlayerDeathEvent();
            }
            else
            {
                //TODO: DEATH
                Destroy(gameObject);
            }
        }
    }   

}

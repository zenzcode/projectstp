using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image healthBarFillImage;

    private void OnEnable()
    {
        EventHandler.PlayerDamagedEvent += PlayerHealthUpdated;
    }

    private void OnDisable()
    {
        EventHandler.PlayerDamagedEvent -= PlayerHealthUpdated;
    }

    private void PlayerHealthUpdated(float newHealth)
    {
        healthBarFillImage.fillAmount = newHealth / Settings.MaxHealth;

        if (healthBarFillImage.fillAmount == 0) healthBarFillImage.fillAmount = 1;
    }
}

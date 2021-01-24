using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image Healthbar;
    private void Awake()
    {
        Healthbar = GetComponent<Image>();
    }
    private void OnEnable()
    {
        EventManager.TakeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        EventManager.TakeDamage -= TakeDamage;
    }
    private void TakeDamage(float maxHealth, float currentHealth)
    {
        Healthbar.fillAmount = currentHealth / maxHealth;
    }
}

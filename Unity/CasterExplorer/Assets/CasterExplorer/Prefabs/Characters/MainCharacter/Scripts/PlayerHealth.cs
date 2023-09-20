using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // ћаксимальное здоровье персонажа
    private int currentHealth; // “екущее здоровье персонажа

    public Image playerHealthFront; // ѕолоска здоровь€ визуализации

    void Start()
    {
        currentHealth = maxHealth; // ”становка начального здоровь€
        UpdateHealthUI(); // ќбновление полоски здоровь€ при старте
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // ¬ычитаем из текущего здоровь€ полученный урон

        if (currentHealth <= 0)
        {
            currentHealth = 0; // «апрещаем отрицательное здоровье
            Die(); // ≈сли здоровье стало меньше или равно нулю, персонаж умирает
        }

        UpdateHealthUI(); // ќбновление полоски здоровь€ после получени€ урона
    }

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount; // ”станавливаем заполнение полоски здоровь€ в соответствии с текущим здоровьем
    }

    void Die()
    {
        Debug.Log("Character has died.");
        // ћожно добавить здесь логику дл€ обработки смерти персонажа,
        // например, перезагрузку уровн€ или активацию анимации смерти.
    }
}


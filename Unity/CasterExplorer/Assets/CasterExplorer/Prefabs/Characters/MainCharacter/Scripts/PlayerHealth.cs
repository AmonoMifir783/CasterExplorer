using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье персонажа
    private int currentHealth; // Текущее здоровье персонажа
    public Image PlayerHealthFront;

    void Start()
    {
        currentHealth = maxHealth; // Установка начального здоровья
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Вычитаем из текущего здоровья полученный урон

        if (currentHealth <= 0)
        {
            Die(); // Если здоровье стало меньше или равно нулю, персонаж умирает
        }
    }

    void Die()
    {
        Debug.Log("Character has died.");
        // Можно добавить здесь логику для обработки смерти персонажа, 
        // например, перезагрузку уровня или активацию анимации смерти.
    }

}

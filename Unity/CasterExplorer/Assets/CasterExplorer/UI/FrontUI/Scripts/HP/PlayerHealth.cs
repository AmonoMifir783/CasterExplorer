using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное здоровье персонажа
    private int currentHealth; // Текущее здоровье персонажа
    public GameObject gameOverPanel; // ссылка на панель "игра окончена"

    public GameObject Player;

    //private bool isGameOver = false; // флаг, указывающий на то, что игра окончена

    public Image playerHealthFront; // Полоска здоровья визуализации

    void Start()
    {
        currentHealth = maxHealth; // Установка начального здоровья
        UpdateHealthUI(); // Обновление полоски здоровья при старте
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Вычитаем из текущего здоровья полученный урон

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Запрещаем отрицательное здоровье
            Die(); // Если здоровье стало меньше или равно нулю, персонаж умирает
        }

        UpdateHealthUI(); // Обновление полоски здоровья после получения урона
    }

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount; // Устанавливаем заполнение полоски здоровья в соответствии с текущим здоровьем
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // загружаем сцену с игрой заново
        gameOverPanel.SetActive(false); // деактивируем панель "игра окончена"
                                        // isGameOver = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // загружаем главное меню
    }

    //private void Update()
    //{
    //    // проверяем, если игра окончена и нажата кнопка выхода
    //    if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        MainMenu(); // загружаем главное меню
    //    }
    //}

    void Die()
    {
        // isGameOver = true;
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Character has died.");
        // Можно добавить здесь логику для обработки смерти персонажа,
        // например, перезагрузку уровня или активацию анимации смерти.
    }
}


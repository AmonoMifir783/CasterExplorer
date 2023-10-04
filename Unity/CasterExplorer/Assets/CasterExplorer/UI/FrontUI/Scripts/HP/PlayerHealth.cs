using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int maxHealth = 100; // ћаксимальное здоровье персонажа
    public int currentHealth; // “екущее здоровье персонажа ЅџЋќ private
    public GameObject gameOverPanel; // ссылка на панель "игра окончена"

    public GameObject Player;

    //private bool isGameOver = false; // флаг, указывающий на то, что игра окончена

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

    public void TakeHeal(int healthToAdd)
    {
        currentHealth += healthToAdd; // ¬ычитаем из текущего здоровь€ полученный урон
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        //if (currentHealth <= 0)
        //{
        //    currentHealth = 100; // «апрещаем отрицательное здоровье
        //    Die(); // ≈сли здоровье стало меньше или равно нулю, персонаж умирает
        //}

        UpdateHealthUI(); // ќбновление полоски здоровь€ после получени€ урона
    }

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount; // ”станавливаем заполнение полоски здоровь€ в соответствии с текущим здоровьем
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
    //    // провер€ем, если игра окончена и нажата кнопка выхода
    //    if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        MainMenu(); // загружаем главное меню
    //    }
    //}

    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
        playerHealthFront.fillAmount = data.playerHealthFillAmount;
    }

    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
        data.playerHealthFillAmount = playerHealthFront.fillAmount;
    }


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
        // ћожно добавить здесь логику дл€ обработки смерти персонажа,
        // например, перезагрузку уровн€ или активацию анимации смерти.
    }

    
}


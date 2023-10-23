using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;
    private float currentShakeDuration = 0f;
    private float currentShakeMagnitude = 0f;
    private Vector3 originalPosition;
    public GameObject gameOverPanel;
    public GameObject Player;
    public Image playerHealthFront;
    private CameraShake cameraShake;
    private PlayerMovement playerMovement;
    Quaternion initialRotation;
    private bool isCharacterDead = false;

    void Start()
    {
        cameraShake = GetComponent<CameraShake>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        if (currentHealth == 0)
        {
            currentHealth = 100;
        }
        UpdateHealthUI();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            if (currentShakeDuration > 0)
            {
                Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * currentShakeMagnitude;

                // Apply the random offset to the camera position
                transform.rotation = transform.rotation * Quaternion.Euler(randomOffset);

                // Reduce the shake duration over time
                currentShakeDuration -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        ShakeCamera();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    public void TakeHeal(int healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadData(GameData data)
    {
        this.maxHealth = data.maxHealth;
        this.currentHealth = data.currentHealth;
        playerHealthFront.fillAmount = data.playerHealthFillAmount;
    }

    public void SaveData(GameData data)
    {
        data.maxHealth = this.maxHealth;
        data.currentHealth = this.currentHealth;
        data.playerHealthFillAmount = playerHealthFront.fillAmount;
    }

    void Die()
    {
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        playerMovement.enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Character has died.");
        isCharacterDead = true;
    }

    public void ShakeCamera()
    {
        if (isCharacterDead)
        {
            return;
        }
        currentShakeDuration = shakeDuration;
        currentShakeMagnitude = shakeMagnitude;
    }
}
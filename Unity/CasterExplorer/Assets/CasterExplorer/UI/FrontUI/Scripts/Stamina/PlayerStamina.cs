using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class PlayerStamina : MonoBehaviour, IDataPersistence
{
    public int maxStamina = 100;
    public int currentStamina = 100;
    public GameObject Player;
    public Image playerStaminaFront;
    private PlayerMovement playerMovement;
    private bool isResting = false;
    private bool isRunningOrJumping = false; // Variable to track if the player is running or jumping
    private float lastActionTime = 0f; // Variable to track the last time the player performed an action (running or jumping)
    private float recoveryTimer;
    public ShootSpell spawnSpell;


    private bool cheatEnabled = false;
    public AudioClip[] cheatSound;
    public AudioSource audioSource;
    private bool hasCheatSound = false;

    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        if (currentStamina == 0)
        {
            currentStamina = 100;
        }
        UpdateStaminaUI();
        spawnSpell = FindAnyObjectByType<ShootSpell>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.C))
        {
            cheatEnabled = !cheatEnabled; // Переключаем состояние чита при нажатии сочетания клавиш X и C
            if (cheatEnabled)
            {
                maxStamina = 1000000; // Устанавливаем максимальное значение для maxHealth
                currentStamina = 1000000; // Устанавливаем максимальное значение для currentHealth
                if (cheatSound.Length > 0 && audioSource != null && !hasCheatSound)
                {
                    int randomIndex = UnityEngine.Random.Range(0, cheatSound.Length);
                    audioSource.PlayOneShot(cheatSound[randomIndex]);
                    hasCheatSound = true;
                }
            }
            else
            {
                maxStamina = 100; // Возвращаем maxHealth к значению 100
                currentStamina = 100; // Возвращаем currentHealth к значению 100
                if (cheatSound.Length > 0 && audioSource != null && hasCheatSound)
                {
                    int randomIndex = UnityEngine.Random.Range(0, cheatSound.Length);
                    audioSource.PlayOneShot(cheatSound[randomIndex]);
                    hasCheatSound = false;
                }
            }
            UpdateStaminaUI();
        }
        // Check if the player is running or jumping
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetButton("Jump");
        bool isShooting = Input.GetButton("Fire1");

        // If the player is running or jumping, update the last action time and set isRunningOrJumping to true
        if (isRunning || isJumping || isShooting)
        {
            lastActionTime = Time.time;
            isRunningOrJumping = true;
            recoveryTimer = 0f;
        }

        // If the player is not running or jumping, start the stamina recovery timer
        if (!isRunningOrJumping)
        {
            if (Time.time - lastActionTime >= 3f && !isResting)
            {
                isResting = true;
                StartCoroutine(RecoverStamina(0f, 10));
            }
        }
        else
        {
            if (isResting)
            {
                StopCoroutine(RecoverStamina(0f, 10)); // Stop the stamina recovery coroutine if the player starts running or jumping again
                isResting = false;
            }
            isRunningOrJumping = false;
        }
    }

    public void TakeFatigue(int fatigueAmount)
    {
        currentStamina -= fatigueAmount;
        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        float fillAmount = (float)currentStamina / maxStamina;
        playerStaminaFront.fillAmount = fillAmount;
    }

    IEnumerator RecoverStamina(float delay, int recoveryRate)
    {
        yield return new WaitForSeconds(delay);
        while (currentStamina < maxStamina)
        {
            if (currentStamina >= maxStamina)
            {
                isResting = false;
                yield break;
            }

            // Check if the player is moving (running or jumping)
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            bool isJumping = Input.GetButton("Jump");

            if (isRunning || isJumping)
            {
                yield return new WaitForSeconds(0.1f); // Wait for 0.1 second if the player is moving
                recoveryTimer = 0f;
                continue;
            }

            yield return new WaitForSeconds(1f);
            recoveryTimer += 1f;

            if (recoveryTimer >= 2.5f)
            {
                currentStamina += recoveryRate;
                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }
                UpdateStaminaUI();
                recoveryTimer = 0f;
            }
        }
    }
    public void LoadData(GameData data)
    {
        this.maxStamina = data.maxStamina;
        this.currentStamina = data.currentStamina;
        playerStaminaFront.fillAmount = data.playerStaminaFillAmount;
    }

    public void SaveData(GameData data)
    {
        data.maxStamina = this.maxStamina;
        data.currentStamina = this.currentStamina;
        data.playerStaminaFillAmount = playerStaminaFront.fillAmount;
    }
}
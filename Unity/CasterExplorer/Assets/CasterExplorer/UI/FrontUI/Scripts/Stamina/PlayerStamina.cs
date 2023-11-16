using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerStamina : MonoBehaviour
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

    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        if (currentStamina == 0)
        {
            currentStamina = 100;
        }
        UpdateStaminaUI();
    }

    void Update()
    {
        // Check if the player is running or jumping
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetButton("Jump");

        // If the player is running or jumping, update the last action time and set isRunningOrJumping to true
        if (isRunning || isJumping)
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

            if (recoveryTimer >= 3f)
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int maxHealth = 100; // ������������ �������� ���������
    public int currentHealth = 100; // ������� �������� ��������� ���� private
    public GameObject gameOverPanel; // ������ �� ������ "���� ��������"

    public GameObject Player;

    //private bool isGameOver = false; // ����, ����������� �� ��, ��� ���� ��������

    public Image playerHealthFront; // ������� �������� ������������

    void Start()
    {
        if (currentHealth == 0)
        {
            currentHealth = 100;
        }
        //currentHealth = maxHealth; // ��������� ���������� ��������
        UpdateHealthUI(); // ���������� ������� �������� ��� ������
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // �������� �� �������� �������� ���������� ����

        if (currentHealth <= 0)
        {
            currentHealth = 0; // ��������� ������������� ��������
            Die(); // ���� �������� ����� ������ ��� ����� ����, �������� �������
        }

        UpdateHealthUI(); // ���������� ������� �������� ����� ��������� �����
    }

    public void TakeHeal(int healthToAdd)
    {
        currentHealth += healthToAdd; // �������� �� �������� �������� ���������� ����
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        //if (currentHealth <= 0)
        //{
        //    currentHealth = 100; // ��������� ������������� ��������
        //    Die(); // ���� �������� ����� ������ ��� ����� ����, �������� �������
        //}

        UpdateHealthUI(); // ���������� ������� �������� ����� ��������� �����
    }

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount; // ������������� ���������� ������� �������� � ������������ � ������� ���������
    }

    //public void LoadGame()
    //{
    //    SceneManager.LoadScene("SampleScene"); // ��������� ����� � ����� ������
    //    gameOverPanel.SetActive(false); // ������������ ������ "���� ��������"
    //                                    // isGameOver = false;
    //}
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // ��������� ������� ����
    }

    //private void Update()
    //{
    //    // ���������, ���� ���� �������� � ������ ������ ������
    //    if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        MainMenu(); // ��������� ������� ����
    //    }
    //}

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
        // isGameOver = true;
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Character has died.");
        // ����� �������� ����� ������ ��� ��������� ������ ���������,
        // ��������, ������������ ������ ��� ��������� �������� ������.
    }

    
}


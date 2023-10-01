using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int maxHealth = 100; // ������������ �������� ���������
    public int currentHealth; // ������� �������� ��������� ���� private
    public GameObject gameOverPanel; // ������ �� ������ "���� ��������"

    public GameObject Player;

    //private bool isGameOver = false; // ����, ����������� �� ��, ��� ���� ��������

    public Image playerHealthFront; // ������� �������� ������������

    void Start()
    {
        currentHealth = maxHealth; // ��������� ���������� ��������
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

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount; // ������������� ���������� ������� �������� � ������������ � ������� ���������
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // ��������� ����� � ����� ������
        gameOverPanel.SetActive(false); // ������������ ������ "���� ��������"
                                        // isGameOver = false;
    }
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
        this.currentHealth = data.currentHealth;
    }

    public void SaveData(ref GameData data)
    {
        data.currentHealth = this.currentHealth;
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


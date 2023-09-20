using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������������ �������� ���������
    private int currentHealth; // ������� �������� ���������

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

    void Die()
    {
        Debug.Log("Character has died.");
        // ����� �������� ����� ������ ��� ��������� ������ ���������,
        // ��������, ������������ ������ ��� ��������� �������� ������.
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // ������������ �������� ���������
    private int currentHealth; // ������� �������� ���������
    public Image PlayerHealthFront;

    void Start()
    {
        currentHealth = maxHealth; // ��������� ���������� ��������
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // �������� �� �������� �������� ���������� ����

        if (currentHealth <= 0)
        {
            Die(); // ���� �������� ����� ������ ��� ����� ����, �������� �������
        }
    }

    void Die()
    {
        Debug.Log("Character has died.");
        // ����� �������� ����� ������ ��� ��������� ������ ���������, 
        // ��������, ������������ ������ ��� ��������� �������� ������.
    }

}

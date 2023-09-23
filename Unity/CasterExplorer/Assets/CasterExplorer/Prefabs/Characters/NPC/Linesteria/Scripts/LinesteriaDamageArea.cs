using UnityEngine;

public class LinesteriaDamageArea : MonoBehaviour
{
    public int damageAmount = 10; // ���������� �����, ������� ����� �������� � ����
    private bool isPlayerInside = false; // ����, �����������, ��������� �� ����� � ����
    private float nextDamageTime = 0f; // ����� ���������� ��������� �����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true; // ������������� ����, ��� ����� ����� � ����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false; // ������������� ����, ��� ����� ����� �� ����
        }
    }

    void Update()
    {
        if (isPlayerInside && Time.time >= nextDamageTime)
        {
            // �������� ����� TakeDamage() � ������� ���������,
            // ����� ������� ���� ������
            PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(damageAmount);

            // ������������� ����� ����� ���������� ��������� ����� � ������ �������� � 5 ������
            nextDamageTime = Time.time + 2f;
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomSaper : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageAmount = 100;

    // ���������� ��� ��������� ������� � ���������
    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������, �������� � ���������, �������
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(damageAmount);
            Debug.Log("mina");
        }
    }
}
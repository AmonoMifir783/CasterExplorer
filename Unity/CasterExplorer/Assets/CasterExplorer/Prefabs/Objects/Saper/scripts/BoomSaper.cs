using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomSaper : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageAmount = 100;

    // Вызывается при попадании игроком в коллайдер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект, попавший в коллайдер, игроком
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(damageAmount);
            Debug.Log("mina");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuddentlyDeath : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public GameObject gameOverPanel;
    public Image playerHealthFront;

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die();
            UpdateHealthUI();
        }
    }

    private void Die()
    {
        playerHealth.currentHealth = 0;
        GameObject playerObject = GameObject.Find("Player");
        playerObject.GetComponent<MouseLook>().enabled = false;
        playerObject.GetComponent<PlayerMovement>().enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    public void UpdateHealthUI()
    {
        float fillAmount = 0;
        playerHealthFront.fillAmount = fillAmount;
    }
}
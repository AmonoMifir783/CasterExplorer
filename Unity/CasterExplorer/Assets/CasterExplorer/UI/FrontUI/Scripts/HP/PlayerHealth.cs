using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;
    private float currentShakeDuration = 0f;
    private float currentShakeMagnitude = 0f;
    private Vector3 originalPosition;
    public GameObject gameOverPanel;
    public GameObject Player;
    public Image playerHealthFront;
    private PlayerMovement playerMovement;
    Quaternion initialRotation;
    private bool isCharacterDead = false;
    public AudioClip[] damageSounds; // массив звуков получения урона
    public AudioClip deathSound;
    public AudioSource audioSource;
    //public AudioSource deathAudioSource;

    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        if (currentHealth == 0)
        {
            currentHealth = 100;
        }
        UpdateHealthUI();
    }

    void LateUpdate()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();

        }

        else
        {
            if (currentShakeDuration > 0)
            {
                Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * currentShakeMagnitude;
                // Apply the random offset to the camera position
                transform.rotation = transform.rotation * Quaternion.Euler(randomOffset);
                // Reduce the shake duration over time
                currentShakeDuration -= Time.unscaledDeltaTime;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        ShakeCamera();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Play a random damage sound
        if (damageSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, damageSounds.Length);
            audioSource.clip = damageSounds[randomIndex];
            audioSource.Play();
        }


        UpdateHealthUI();
    }

    public void TakeHeal(int healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        playerHealthFront.fillAmount = fillAmount;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

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
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        playerMovement.enabled = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Character has died.");
        isCharacterDead = true;

        // Play the death sound
        //if (deathSound != null && deathAudioSource != null)
        //{
        //    deathAudioSource.clip = deathSound;
        //    deathAudioSource.Play();
        //}
    }

    public void ShakeCamera()
    {
        if (isCharacterDead)
        {
            return;
        }
        currentShakeDuration = shakeDuration;
        currentShakeMagnitude = shakeMagnitude;
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using TMPro;
//using System;

//public class PlayerHealth : MonoBehaviour, IDataPersistence
//{
//    public int maxHealth = 100;
//    public int currentHealth = 100;
//    public float shakeDuration = 0.1f;
//    public float shakeMagnitude = 0.1f;
//    private float currentShakeDuration = 0f;
//    private float currentShakeMagnitude = 0f;
//    private Vector3 originalPosition;
//    public GameObject gameOverPanel;
//    public GameObject Player;
//    public Image playerHealthFront;
//    private PlayerMovement playerMovement;
//    Quaternion initialRotation;
//    private bool isCharacterDead = false;
//    public AudioClip[] damageSounds; // массив звуков получения урона
//    public AudioClip deathSound;
//    public AudioSource audioSource;
//    private bool isCameraFalling = false;
//    void Start()
//    {
//        playerMovement = Player.GetComponent<PlayerMovement>();
//        if (currentHealth == 0)
//        {
//            currentHealth = 100;
//        }
//        UpdateHealthUI();
//    }

//    void LateUpdate()
//    {
//        if (currentHealth <= 0)
//        {
//            currentHealth = 0;
//            Die();

//        }

//        if (isCameraFalling)
//        {
//            if (currentShakeDuration > 0)
//            {
//                Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * currentShakeMagnitude;
//                // Apply the random offset to the camera position
//                transform.rotation = transform.rotation * Quaternion.Euler(randomOffset);
//                // Reduce the shake duration over time
//                currentShakeDuration -= Time.unscaledDeltaTime;
//            }
//            else
//            {
//                // Stop the camera falling coroutine when it reaches the target position
//                isCameraFalling = false;
//                StopCoroutine(FallCamera());
//            }
//        }
//    }

//    public void TakeDamage(int damageAmount)
//    {
//        currentHealth -= damageAmount;
//        ShakeCamera();

//        if (currentHealth <= 0)
//        {
//            currentHealth = 0;
//            Die();
//        }

//        // Play a random damage sound
//        if (damageSounds.Length > 0 && audioSource != null)
//        {
//            int randomIndex = UnityEngine.Random.Range(0, damageSounds.Length);
//            audioSource.clip = damageSounds[randomIndex];
//            audioSource.Play();
//        }


//        UpdateHealthUI();
//    }

//    public void TakeHeal(int healthToAdd)
//    {
//        currentHealth += healthToAdd;
//        if (currentHealth > 100)
//        {
//            currentHealth = 100;
//        }
//        UpdateHealthUI();
//    }

//    private void UpdateHealthUI()
//    {
//        float fillAmount = (float)currentHealth / maxHealth;
//        playerHealthFront.fillAmount = fillAmount;
//    }

//    public void MainMenu()
//    {
//        SceneManager.LoadScene("Menu");
//    }

//    public void LoadData(GameData data)
//    {
//        this.maxHealth = data.maxHealth;
//        this.currentHealth = data.currentHealth;
//        playerHealthFront.fillAmount = data.playerHealthFillAmount;
//    }

//    public void SaveData(GameData data)
//    {
//        data.maxHealth = this.maxHealth;
//        data.currentHealth = this.currentHealth;
//        data.playerHealthFillAmount = playerHealthFront.fillAmount;
//    }



//    public void ShakeCamera()
//    {
//        if (isCharacterDead)
//        {
//            return;
//        }
//        currentShakeDuration = shakeDuration;
//        currentShakeMagnitude = shakeMagnitude;
//    }
//    void Die()
//    {
//        Player.GetComponent<MouseLook>().enabled = false;
//        Player.GetComponent<PlayerMovement>().enabled = false;
//        playerMovement.enabled = false;
//        gameOverPanel.SetActive(true);
//        StartCoroutine(FallCamera());
//        StartCoroutine(GameOver());
//    }

//    IEnumerator FallCamera()
//    {
//        Vector3 originalPosition = transform.position;
//        Vector3 targetPosition = originalPosition - new Vector3(0f, 10f, 0f); // Adjust the Y value as needed
//        float elapsedTime = 0f;
//        float duration = 1f; // Adjust the duration as needed
//        while (elapsedTime < duration)
//        {
//            elapsedTime += Time.deltaTime;
//            float t = Mathf.Clamp01(elapsedTime / duration);
//            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
//            yield return null;
//        }
//    }

//    IEnumerator GameOver()
//    {
//        yield return new WaitForSeconds(2f); // Adjust the delay as needed
//        Time.timeScale = 0f;
//        Cursor.visible = true;
//        Cursor.lockState = CursorLockMode.None;
//    }
//}
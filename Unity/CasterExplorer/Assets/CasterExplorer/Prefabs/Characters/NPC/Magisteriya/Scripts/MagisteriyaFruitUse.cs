
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagisteriyaFruitUse : MonoBehaviour
{
    public int healthToAdd = 25;
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;
    private PickUp pickUp;
    public TextMeshProUGUI MagisteriyaCount;
    public AudioClip[] eatSounds; // Array of eat sounds
    public AudioSource audioSource;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            pickUp = playerObject.GetComponent<PickUp>();
            // if (magisteriyaPickUpScript == null)
            // {
            //     Debug.LogError("MagisteriyaFruitPickUp script not found on Player object.");
            // }
        }
        else
        {
            Debug.LogError("Player object not found with tag 'Player'.");
        }
    }

    private void Update()
    {
        if (pickUp != null)
        {
            int inventoryCount = pickUp.inventoryCount;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (inventoryCount >= 0)
                {
                    //ApplyItem(inventoryCount);
                    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                    PlayerHealth playerHealthScript = playerObject.GetComponent<PlayerHealth>();
                    if (inventoryCount > 0 && playerHealthScript.currentHealth != 100)
                    {
                        MagisteriyaCount.text = (inventoryCount - 1).ToString();
                    }
                    ApplyItem(inventoryCount);
                }
                
            }
        }

    }

    private void ApplyItem(int count)
    {
        if (count > 0)
        {
            count--;
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                PlayerHealth playerHealthScript = playerObject.GetComponent<PlayerHealth>();
                if (playerHealthScript != null)
                {
                    if (playerHealthScript.currentHealth < 100) // Check if the player's current health is less than 100
                    {
                        playerHealthScript.TakeHeal(healthToAdd);
                        pickUp.inventoryCount = count;
                        Debug.Log("Item applied! Current inventory count: " + count);
                        // Add additional code for using the item or modifying player health here.

                        // Play one of the eat sounds randomly
                        if (eatSounds.Length > 0)
                        {
                            int randomIndex = Random.Range(0, eatSounds.Length);
                            audioSource.PlayOneShot(eatSounds[randomIndex]);
                        }
                    }
                    else
                    {
                        Debug.Log("Cannot use item. Player's health is already at maximum.");
                    }
                }
                else
                {
                    Debug.LogError("PlayerHealth script not found on Player object.");
                }
            }
            else
            {
                Debug.LogError("Player object not found with tag 'Player'.");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagisteriyaFruitUse : MonoBehaviour
{
    public int healthToAdd = 25;
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;
    public TextMeshProUGUI MagisteriyaCount;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            magisteriyaPickUpScript = playerObject.GetComponent<MagisteriyaFruitPickUp>();
            if (magisteriyaPickUpScript == null)
            {
                Debug.LogError("MagisteriyaFruitPickUp script not found on Player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found with tag 'Player'.");
        }
    }

    private void Update()
    {
        if (magisteriyaPickUpScript != null)
        {
            int inventoryCount = magisteriyaPickUpScript.inventoryCount;
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
                        magisteriyaPickUpScript.inventoryCount = count;
                        Debug.Log("Item applied! Current inventory count: " + count);
                        // Add additional code for using the item or modifying player health here.
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
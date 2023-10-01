using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagisteriyaFruitUse : MonoBehaviour
{
    public int healthToAdd = 25;
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;

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
                ApplyItem(inventoryCount);
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
                    playerHealthScript.TakeHeal(healthToAdd);
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

            magisteriyaPickUpScript.inventoryCount = count;
            Debug.Log("Item applied! Current inventory count: " + count);
            // Add additional code for using the item or modifying player health here.
        }
    }
}

//3 вариант готовый
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class PickMagisteriya : MonoBehaviour
{

    public PickUp pickUp;
    private bool canPickUp = false; // bil private
    private bool isPickingUp = false; // Flag to track if the player is in the process of picking up an item
    public GameObject pickedUpFruit; // Reference to the fruit object being picked up
    private GameObject lastPickedUpFruit; // Reference to the last picked up fruit object
    private List<GameObject> pickedUpFruits = new List<GameObject>(); // List to keep track of picked up fruits
    public bool fruitPicked = false;
    //public MagisteriyaCollected magisteriyaCollectedScript;
    public GameObject Fruit;

    private void Start()
    {
        pickUp = FindObjectOfType<PickUp>();
    }
    private void Update()
    {
        //CheckForPickUp();
    }

    //private void CheckForPickUp()
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && !isPickingUp) // Check if the player is not already picking up an item
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
    //        {
    //            if (hit.collider.CompareTag("Fruit") && hit.collider.enabled && hit.collider.gameObject != lastPickedUpFruit && !pickedUpFruits.Contains(hit.collider.gameObject)) // Check if the collider is enabled, the fruit object is not the last picked up fruit, and has not been picked up before
    //            {
    //                canPickUp = true;
    //                pickedUpFruit = hit.collider.gameObject; // Store the reference to the fruit object being picked up
    //                fruitPicked = true;
    //                PickUpItem();
    //            }
    //        }
    //        else
    //        {
    //            canPickUp = false;
    //        }
    //    }
    //}


    //было private void PickUpItem()
    public void PickUpItem()
    {
        if (pickUp.inventoryCount < 10)
        {
            pickUp.inventoryCount++;
            Debug.Log("Item picked up! Current inventory count: " + pickUp.inventoryCount);
            //if (pickedUpFruit.CompareTag("Fruit"))
            //{
            //    Destroy(pickedUpFruit.GetComponent<Collider>()); // Destroy the collider for the picked-up fruit
            //    pickedUpFruit.GetComponent<Renderer>().enabled = false; // Disable the renderer for the picked-up fruit
            //    Destroy(pickedUpFruit, 1f); // Destroy the picked-up fruit after 1 second
            //    pickedUpFruits.Add(pickedUpFruit); // Add the picked up fruit to the list
            //}
            pickUp.MagisteriyaCount.text = pickUp.inventoryCount.ToString();
            isPickingUp = true; // Set the flag to indicate that the player is in the process of picking up an item
            lastPickedUpFruit = pickedUpFruit; // Update the reference to the last picked up fruit
            StartCoroutine(ResetPickUpFlag()); // Start a coroutine to reset the flag after a certain delay
            fruitPicked = true;
            Destroy(Fruit);
        }
        else if (!canPickUp)
        {
            Debug.Log("Cannot pick up item. Move closer to the item.");

        }
        else
        {
            Debug.Log("Inventory is full! Cannot pick up item.");

        }
    }



    //public void LoadData(GameData data)
    //{
    //    this.inventoryCount = data.inventoryCount;
    //    this.MagisteriyaCount.text = data.MagisteriyaCount;

    //    // Clear the list of picked up fruits before loading
    //    pickedUpFruits.Clear();

    //    foreach (string fruitName in data.magisteriyaCollected.Keys)
    //    {
    //        bool isPickedUp = data.magisteriyaCollected[fruitName];

    //        if (isPickedUp)
    //        {
    //            GameObject fruit = GameObject.Find(fruitName);
    //            if (fruit != null)
    //            {
    //                pickedUpFruits.Add(fruit);
    //                fruit.SetActive(false); // Disable the fruit object to prevent it from appearing in the game
    //            }
    //        }
    //    }
    //}

    //public void SaveData(GameData data)
    //{
    //    data.inventoryCount = this.inventoryCount;
    //    data.MagisteriyaCount = this.MagisteriyaCount.text;
    //    data.magisteriyaCollected = new SerializableDictionary<string, bool>();

    //    foreach (GameObject fruit in pickedUpFruits)
    //    {
    //        if (fruit != null && !data.magisteriyaCollected.ContainsKey(fruit.name))
    //        {
    //            data.magisteriyaCollected.Add(fruit.name, true);
    //            fruit.SetActive(false);
    //        }
    //    }
    //}




    private IEnumerator ResetPickUpFlag()
    {
        yield return new WaitForSecondsRealtime(0f); // Adjust the delay as needed
        isPickingUp = false; // Reset the flag after the delay
        pickedUpFruit = null; // Reset the reference to the fruit object being picked up
    }
}

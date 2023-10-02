//1 версия колайдер
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using TMPro;


//public class MagisteriyaFruitPickUp : MonoBehaviour
//{
//    // Количество здоровья, добавляемое при использовании предмета
//    public int inventoryCount = 0; // Количество предметов в инвентаре
//    public TextMeshProUGUI MagisteriyaCount;

//    private bool canPickUp = false; // Флаг, указывающий, можно ли подобрать предмет

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Fruit"))
//        {
//            canPickUp = true;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Fruit"))
//        {
//            canPickUp = false;
//        }
//    }

//    private void PickUpItem()
//    {
//        if (canPickUp && inventoryCount < 10) // Проверка, можно ли подобрать и не превышает ли количество предметов лимит
//        {
//            inventoryCount++; // Увеличиваем количество предметов в инвентаре при подборе
//            Debug.Log("Item picked up! Current inventory count: " + inventoryCount);
//            GameObject[] Fruits = GameObject.FindGameObjectsWithTag("Fruit");
//            foreach (GameObject Fruit in Fruits)
//            {
//                if (Fruit == gameObject)
//                {
//                    Destroy(Fruit.gameObject); // Удаляем предмет с карты
//                    break;
//                }
//            }
//        }
//        else if (!canPickUp)
//        {
//            Debug.Log("Cannot pick up item. Move closer to the item.");
//        }
//        else
//        {
//            Debug.Log("Inventory is full! Cannot pick up item.");
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            PickUpItem();
//            MagisteriyaCount.text = "" + inventoryCount;
//        }
//    }
//}


//2 версия рейкаст
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using TMPro;
//using System;

//public class MagisteriyaFruitPickUp : MonoBehaviour
//{
//    public int inventoryCount = 0;
//    public TextMeshProUGUI MagisteriyaCount;
//    private bool canPickUp = false;

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            RaycastHit hit;
//            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
//            {
//                if (hit.collider.CompareTag("Fruit"))
//                {
//                    canPickUp = true;
//                }
//            }
//            else
//            {
//                canPickUp = false;
//            }

//            if (canPickUp && inventoryCount < 10)
//            {
//                inventoryCount++;
//                Debug.Log("Предмет подобран! Текущее количество в инвентаре: " + inventoryCount);

//                if (hit.collider.CompareTag("Fruit"))
//                {
//                    Destroy(hit.collider.gameObject);
//                }

//                MagisteriyaCount.text = inventoryCount.ToString();
//            }
//            else if (!canPickUp)
//            {
//                Debug.Log("Невозможно подобрать предмет. Подойдите ближе к предмету.");
//            }
//            else
//            {
//                Debug.Log("Инвентарь полон! Невозможно подобрать предмет.");
//            }
//        }
//    }
//}


//3 вариант готовый
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MagisteriyaFruitPickUp : MonoBehaviour
{
    public int inventoryCount = 0;
    public TextMeshProUGUI MagisteriyaCount;
    private bool canPickUp = false;
    private bool isPickingUp = false; // Flag to track if the player is in the process of picking up an item
    private GameObject pickedUpFruit; // Reference to the fruit object being picked up
    private GameObject lastPickedUpFruit; // Reference to the last picked up fruit object
    private List<GameObject> pickedUpFruits = new List<GameObject>(); // List to keep track of picked up fruits

    private void Update()
    {
        CheckForPickUp();
    }

    private void CheckForPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPickingUp) // Check if the player is not already picking up an item
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if (hit.collider.CompareTag("Fruit") && hit.collider.enabled && hit.collider.gameObject != lastPickedUpFruit && !pickedUpFruits.Contains(hit.collider.gameObject)) // Check if the collider is enabled, the fruit object is not the last picked up fruit, and has not been picked up before
                {
                    canPickUp = true;
                    pickedUpFruit = hit.collider.gameObject; // Store the reference to the fruit object being picked up
                    PickUpItem();
                }
            }
            else
            {
                canPickUp = false;
            }
        }
    }

    private void PickUpItem()
    {
        if (canPickUp && inventoryCount < 10)
        {
            inventoryCount++;
            Debug.Log("Item picked up! Current inventory count: " + inventoryCount);
            if (pickedUpFruit.CompareTag("Fruit"))
            {
                Destroy(pickedUpFruit.GetComponent<Collider>()); // Destroy the collider for the picked-up fruit
                pickedUpFruit.GetComponent<Renderer>().enabled = false; // Disable the renderer for the picked-up fruit
                Destroy(pickedUpFruit, 1f); // Destroy the picked-up fruit after 1 second
                pickedUpFruits.Add(pickedUpFruit); // Add the picked up fruit to the list
            }
            MagisteriyaCount.text = inventoryCount.ToString();
            isPickingUp = true; // Set the flag to indicate that the player is in the process of picking up an item
            lastPickedUpFruit = pickedUpFruit; // Update the reference to the last picked up fruit
            StartCoroutine(ResetPickUpFlag()); // Start a coroutine to reset the flag after a certain delay
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

    private IEnumerator ResetPickUpFlag()
    {
        yield return new WaitForSecondsRealtime(1f); // Adjust the delay as needed
        isPickingUp = false; // Reset the flag after the delay
        pickedUpFruit = null; // Reset the reference to the fruit object being picked up
    }
}

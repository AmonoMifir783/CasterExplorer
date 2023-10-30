
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class ChestPickUp : MonoBehaviour, IDataPersistence
{
    public int scrollCount = 5;
    public TextMeshProUGUI ScrollCount;
    private bool canPickUp = false;
    private bool isPickingUp = false;
    public GameObject pickedUpChest;
    private GameObject lastPickedUpChest;
    private List<GameObject> pickedUpChests = new List<GameObject>();
    private MagisteriyaFruitPickUp magisteriyaFruitPickUp;
    public bool chestPicked = false;
    public GameObject Player;

    private void Start()
    {
        magisteriyaFruitPickUp = FindObjectOfType<MagisteriyaFruitPickUp>();
        UpdateScrollCountUI();
    }

    private void Update()
    {
        CheckForPickUp();
    }

    private void CheckForPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPickingUp)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if (hit.collider.CompareTag("Chest") && hit.collider.enabled && hit.collider.gameObject != lastPickedUpChest && !pickedUpChests.Contains(hit.collider.gameObject))
                {
                    canPickUp = true;
                    pickedUpChest = hit.collider.gameObject;
                    chestPicked = true;
                    PickUpItem();
                }
            }
            else
            {
                canPickUp = false;
            }
        }
    }

    public void PickUpItem()
    {
        if (canPickUp && scrollCount < 1000)
        {
            int randomScrolls = UnityEngine.Random.Range(1, 4); // Generate a random number between 1 and 3
            scrollCount += randomScrolls;
            int remainingFruitCapacity = 10 - magisteriyaFruitPickUp.inventoryCount;
            int randomFruits = UnityEngine.Random.Range(1, remainingFruitCapacity + 1); // Generate a random number between 1 and the remaining fruit capacity
            int totalFruits = magisteriyaFruitPickUp.inventoryCount + randomFruits;
            int pickedUpFruits = totalFruits <= 10 ? randomFruits : remainingFruitCapacity;
            magisteriyaFruitPickUp.inventoryCount += pickedUpFruits;
            magisteriyaFruitPickUp.MagisteriyaCount.text = magisteriyaFruitPickUp.inventoryCount.ToString();
            Debug.Log("Item picked up! Current inventory count: " + scrollCount);
            if (pickedUpChest.CompareTag("Chest"))
            {
                Destroy(pickedUpChest.GetComponent<Collider>());
                pickedUpChest.GetComponent<Renderer>().enabled = false;
                Destroy(pickedUpChest, 1f);
                pickedUpChests.Add(pickedUpChest);
            }
            UpdateScrollCountUI();
            isPickingUp = true;
            lastPickedUpChest = pickedUpChest;
            StartCoroutine(ResetPickUpFlag());
            chestPicked = true;
        }
        else if (!canPickUp)
        {
            Debug.Log("Cannot pick up item. Move closer to the item.");
        }
        else if (scrollCount >= 1000)
        {
            Debug.Log("Inventory is full! Cannot pick up item.");
        }
    }

    private void UpdateScrollCountUI()
    {
        ScrollCount.text = scrollCount.ToString();
    }

    public void LoadData(GameData data)
    {
        this.scrollCount = data.scrollCount;
        this.ScrollCount.text = data.ScrollCount;
    }

    public void SaveData(GameData data)
    {
        data.scrollCount = this.scrollCount;
        data.ScrollCount = this.ScrollCount.text;
    }

    private IEnumerator ResetPickUpFlag()
    {
        yield return new WaitForSecondsRealtime(0f);
        isPickingUp = false;
        pickedUpChest = null;
    }
}
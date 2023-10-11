using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MagisteriyaCollected : MonoBehaviour, IDataPersistence
{
    private MagisteriyaFruitPickUp magisteriyaPickUpScript;
    private GameObject pickedUpFruit;
    // private bool collected = false;
    private SpriteRenderer visual;
    public bool fruitPicked = false;


    [SerializeField] private GameObject Magisteriya;
    [SerializeField] public string id; //bilo private
    [ContextMenu("Generate guid for id")]




    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        magisteriyaPickUpScript = GetComponent<MagisteriyaFruitPickUp>();
        
    }



    public void LoadData(GameData data)
    {
        //data.magisteriyaCollected.TryGetValue(id, out magisteriyaPickUpScript.fruitPicked);
        //if (magisteriyaPickUpScript.fruitPicked)
        //{
        //    visual.gameObject.SetActive(false); 
        //}
    }

    public void SaveData(GameData data)
    {
    //    if (data.magisteriyaCollected.ContainsKey(id))
    //    {
    //        data.magisteriyaCollected.Remove(id);
    //    }
    //    data.magisteriyaCollected.Add(id, magisteriyaPickUpScript.fruitPicked);
    }



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using TMPro;
//using System;

//public class MagisteriyaCollected : MonoBehaviour, IDataPersistence
//{
//    public int inventoryCount = 0;
//    public TextMeshProUGUI MagisteriyaCount;
//    private bool canPickUp = false; // bil private
//    private bool isPickingUp = false; // Flag to track if the player is in the process of picking up an item
//    private GameObject pickedUpFruit; // Reference to the fruit object being picked up
//    private GameObject lastPickedUpFruit; // Reference to the last picked up fruit object
//    private List<GameObject> pickedUpFruits = new List<GameObject>(); // List to keep track of picked up fruits
//    private bool fruitPicked = true;
//    [SerializeField] private GameObject Magisteriya;
//    [SerializeField] public string id; //bilo private
//    [ContextMenu("Generate guid for id")]
//    //public MagisteriyaCollected magisteriyaCollectedScript;

//    private void Update()
//    {
//        CheckForPickUp();
//    }

//    private void CheckForPickUp()
//    {
//        if (Input.GetKeyDown(KeyCode.E) && !isPickingUp) // Check if the player is not already picking up an item
//        {
//            RaycastHit hit;
//            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
//            {
//                if (hit.collider.CompareTag("Fruit") && hit.collider.enabled && hit.collider.gameObject != lastPickedUpFruit && !pickedUpFruits.Contains(hit.collider.gameObject)) // Check if the collider is enabled, the fruit object is not the last picked up fruit, and has not been picked up before
//                {
//                    canPickUp = true;
//                    pickedUpFruit = hit.collider.gameObject; // Store the reference to the fruit object being picked up
//                    PickUpItem();
//                }
//            }
//            else
//            {
//                canPickUp = false;
//            }
//        }
//    }


//    //было private void PickUpItem()
//    public void PickUpItem()
//    {
//        if (canPickUp && inventoryCount < 10)
//        {
//            inventoryCount++;
//            Debug.Log("Item picked up! Current inventory count: " + inventoryCount);
//            if (pickedUpFruit.CompareTag("Fruit"))
//            {
//                Destroy(pickedUpFruit.GetComponent<Collider>()); // Destroy the collider for the picked-up fruit
//                pickedUpFruit.GetComponent<Renderer>().enabled = false; // Disable the renderer for the picked-up fruit
//                Destroy(pickedUpFruit, 1f); // Destroy the picked-up fruit after 1 second
//                pickedUpFruits.Add(pickedUpFruit); // Add the picked up fruit to the list
//            }
//            MagisteriyaCount.text = inventoryCount.ToString();
//            isPickingUp = true; // Set the flag to indicate that the player is in the process of picking up an item
//            lastPickedUpFruit = pickedUpFruit; // Update the reference to the last picked up fruit
//            StartCoroutine(ResetPickUpFlag()); // Start a coroutine to reset the flag after a certain delay
//            fruitPicked = false;
//        }
//        else if (!canPickUp)
//        {
//            Debug.Log("Cannot pick up item. Move closer to the item.");
//            fruitPicked = true;
//        }
//        else
//        {
//            Debug.Log("Inventory is full! Cannot pick up item.");
//            fruitPicked = true;
//        }
//    }

//    private void GenerateGuid()
//    {
//        id = System.Guid.NewGuid().ToString();
//    }

//    public void LoadData(GameData data)
//    {
//        this.inventoryCount = data.inventoryCount;
//        this.MagisteriyaCount.text = data.MagisteriyaCount;
//        if (pickedUpFruit)
//        {
//            if (data.magisteriyaCollected.TryGetValue(id, out fruitPicked))
//            {
//                if (pickedUpFruit)
//                {
//                    Magisteriya.gameObject.SetActive(false);
//                }
//                else
//                {
//                    Magisteriya.gameObject.SetActive(true);
//                }
//            }
//            else
//            {
//                if (!pickedUpFruit)
//                {
//                    Magisteriya.gameObject.SetActive(true);
//                }
//            }
//        }

//    }

//    public void SaveData(GameData data)
//    {
//        data.inventoryCount = this.inventoryCount;
//        data.MagisteriyaCount = this.MagisteriyaCount.text;
//        data.magisteriyaCollected[id] = pickedUpFruit;

//    }




//    private IEnumerator ResetPickUpFlag()
//    {
//        yield return new WaitForSecondsRealtime(1f); // Adjust the delay as needed
//        isPickingUp = false; // Reset the flag after the delay
//        pickedUpFruit = null; // Reset the reference to the fruit object being picked up
//    }
//}
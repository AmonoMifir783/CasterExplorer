using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellSystemScript : MonoBehaviour
{
    public bool PauseGame;
    public GameObject SpellSystemMenu;
    public GameObject playerCamera;
    public GameObject Player;
    public GameObject FastSlots;
    public GameObject FrontUI;
    public MurrorFastSlots MurrorFastSlots;

    public GameObject CraftSlot1;
    public GameObject CraftSlot2;
    public GameObject CraftSlot3;

    public InventoryManager inventoryManager;

    public PauseMenu pauseMenu;
    public OpenBestiary openBestiary;
    public bool inventoryon = false;

    private void Start() 
    {
        MurrorFastSlots = FastSlots.GetComponent<MurrorFastSlots>(); 
        inventoryManager = FindObjectOfType<InventoryManager>();
        pauseMenu = GetComponent<PauseMenu>();
        //openBestiary = GetComponent<OpenBestiary>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!PauseGame)
            {
                playerCamera.GetComponent<MouseLook>().enabled = false;
                playerCamera.GetComponent<PlayerMovement>().enabled = false;
               // Player.GetComponent<MagisteriyaFruitPickUp>().enabled = false;
                Player.GetComponent<MagisteriyaFruitUse>().enabled = false;
                //Player.GetComponent<ChestPickUp>().enabled = false;
                Player.GetComponent<PickUp>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                InventoryOn();
            }
            else
            {
                playerCamera.GetComponent<MouseLook>().enabled = true;
                playerCamera.GetComponent<PlayerMovement>().enabled = true;
                //Player.GetComponent<MagisteriyaFruitPickUp>().enabled = true;
                Player.GetComponent<MagisteriyaFruitUse>().enabled = true;
                //Player.GetComponent<ChestPickUp>().enabled = true;
                Player.GetComponent<PickUp>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                InventoryOff();
            }
        }
    }

    public void InventoryOn()
    {
        if (!pauseMenu.isPaused)
        {
            SpellSystemMenu.SetActive(true);
            Time.timeScale = 0f;
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 0f;
            }
            PauseGame = true;
            inventoryon = true;
            Cursor.visible = true;
            //openBestiary.enabled = false;
            FrontUI.SetActive(false);
        }

        // if (GetComponent<Rigidbody>())
        //     GetComponent<Rigidbody>().freezeRotation = true;
    }

    public void InventoryOff()
    {
        if(!CraftSlot1.GetComponent<InventorySlots>().isEmpty)
        {
            ClearCreateSlots(CraftSlot1);
        }
        if(!CraftSlot2.GetComponent<InventorySlots>().isEmpty)
        {
            ClearCreateSlots(CraftSlot2);
        }
        if(!CraftSlot3.GetComponent<InventorySlots>().isEmpty)
        {
            ClearCreateSlots(CraftSlot3);
        }

        SpellSystemMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
        Cursor.visible = false;
        inventoryon = false;

        MurrorFastSlots.CloseInventory();
       // openBestiary.enabled=true;
        FrontUI.SetActive(true);
    }

    void ClearCreateSlots(GameObject CraftSlot)
    {
            CreateNewSlots createNewSlots = FindObjectOfType<CreateNewSlots>();
            createNewSlots.AddItem(CraftSlot.GetComponent<InventorySlots>().Item);

            CraftSlot.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = Color.clear;
            CraftSlot.GetComponent<InventorySlots>().Icon.GetComponent<Image>().sprite = null;
            CraftSlot.GetComponent<InventorySlots>().Item = null;
            CraftSlot.GetComponent<InventorySlots>().isEmpty = true;
            CraftSlot.GetComponent<InventorySlots>().isHighlighting = false;
            inventoryManager.Inicialization();
    }
}

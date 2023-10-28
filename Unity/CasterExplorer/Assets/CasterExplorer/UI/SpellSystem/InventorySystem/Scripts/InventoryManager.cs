using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{

    public GameObject FastSlot1;
    public GameObject FastSlot2;
    public GameObject FastSlot3;
    public GameObject FastSlot4;


    //FastSlot1.GetComponent<InventorySlots>().Item;
    public CreateNewSlots createNewSlots;

    public Transform InventoryPanel;
    public List<InventorySlots> Slots = new List<InventorySlots>();

    public int i = 0;


    void Start()
    {
        createNewSlots = GetComponent<CreateNewSlots>();
    }
    public void Inicialization()
    {
        while (i < InventoryPanel.childCount)
        {
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
            {
                Slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlots>());
                Debug.Log("Slots =" +  Slots[i].Item);
                i++;
            }
        }
    }
    public void LoadData(GameData data)
    {
        for (int i = 1; i < data.Slots.Count; i++)
        {
            ItemScriptableObject[] items = Resources.LoadAll<ItemScriptableObject>("SpellSystem/InventorySystem/Items");
            Debug.Log("dasdas" + items[i]);

            // Iterate through the loaded items
            foreach (ItemScriptableObject item in items)
            {
                if (data.Slots[i] == item.ItemName)
                {
                    CreateNewSlots createNewSlotsScript = GetComponent<CreateNewSlots>();
                    createNewSlotsScript.AddItem(item);
                    Debug.Log(item);
                }
            }
        }
    }
    public void SaveData(GameData data) 
    {
        for (int i = 0; i < InventoryPanel.childCount; i++)
        {
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
            {
                data.Slots.Add(this.Slots[i].Item.ItemName);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

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
                //Debug.Log("Slots =" +  Slots[i].Item);
                i++;
            }
        }
    }
    public void LoadData(GameData data)
    {
        //for (int i = 1; i < data.Slots.Count; i++)
        //{
        //    ItemScriptableObject[] items = Resources.LoadAll<ItemScriptableObject>("SpellSystem/InventorySystem/Items");
        //    //Debug.Log("dasdas" + items[i]);

        //    // Iterate through the loaded items
        //    foreach (ItemScriptableObject item in items)
        //    {
        //        if (data.Slots[i] == item.ItemName)
        //        {
        //            CreateNewSlots createNewSlotsScript = GetComponent<CreateNewSlots>();
        //            createNewSlotsScript.AddItem(item);
        //            //Debug.Log(item);
        //        }
        //    }
        //}
    }
    public void SaveData(GameData data) 
    {
        //Inicialization();
        //Debug.Log("СCount::" + InventoryPanel.childCount);
        //for (int i = 0; i < InventoryPanel.childCount; i++)
        //{
        //    Debug.Log("Childs::" + i + " " + InventoryPanel.GetChild(i));
        //    if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
        //    {
        //        data.SlotsSave.Add(this.Slots[i].Item.ItemName);
        //    }
        //}
        //for (int i = 1; i <= data.SlotsSave.Count; i++)
        //{
        //    var assetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items/"+ i +".asset";

        //    // Загружаем актив с помощью AssetDatabase
        //    var loadedAsset = AssetDatabase.LoadAssetAtPath<ItemScriptableObject>(assetPath);

        //    // Проверяем, что актив был успешно загружен
        //    if (loadedAsset != null)
        //    {
        //        CreateNewSlots createNewSlotsScript = GetComponent<CreateNewSlots>();
        //        createNewSlotsScript.AddItem(loadedAsset);
        //        // Делаем что-то с загруженным активом
        //        Debug.Log("Item asset loaded: " + loadedAsset.name);
        //    }
        //    else
        //    {
        //        Debug.Log("Failed to load item asset at path: " + assetPath);
        //    }
        //}
        //Inicialization();


        Inicialization();
        Debug.Log("СCount::" + InventoryPanel.childCount);
        for (int i = 0; i < InventoryPanel.childCount; i++)
        {
            Debug.Log("Childs::" + i + " " + InventoryPanel.GetChild(i));
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
            {
                data.SlotsSave.Add(this.Slots[i].Item.ItemName);
            }
        }
        for (int i = 1; i <= data.SlotsSave.Count; i++)
        {
            var assetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items/" + i + ".asset";
            // Загружаем актив с помощью AssetDatabase
            var loadedAsset = AssetDatabase.LoadAssetAtPath<ItemScriptableObject>(assetPath);
            // Проверяем, что актив был успешно загружен и является ItemScriptableObject
            if (loadedAsset != null)
            {
                SpellItem spellItem = loadedAsset.GetComponent<SpellItem>();
                if (spellItem != null)
                {
                    // Access properties or methods of the SpellItem script
                    Debug.Log("Item asset loaded: " + spellItem.ItemName);
                }
                else
                {
                    Debug.Log("SpellItem component not found on loadedAsset");
                }
            }
            else
            {
                Debug.Log("Failed to load item asset at path: " + assetPath);
            }
        }
        Inicialization();
    }


}

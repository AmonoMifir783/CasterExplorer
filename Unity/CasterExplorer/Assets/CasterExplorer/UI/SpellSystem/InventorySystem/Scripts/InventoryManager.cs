using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//using UnityEngine.Localization.SmartFormat.Utilities;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public GameObject FastSlot1;
    public GameObject FastSlot2;
    public GameObject FastSlot3;
    public GameObject FastSlot4;
    //FastSlot1.GetComponent<InventorySlots>().Item;
    //public CreateNewSlots createNewSlots;
    public Transform InventoryPanel;
    public List<InventorySlots> Slots = new List<InventorySlots>();
    public Dictionary<string, ItemScriptableObject> spellDictionary = new Dictionary<string, ItemScriptableObject>();

    public int i = 0;

    public bool isLoading = false;

    public GameObject prefab; // ,
                              //public Transform contentPanel;

    public Sprite SpellArt;
    public GameObject CreateSlot;
    public InventorySlots inventorySlots;

    void Start()
    {
        //createNewSlots = CreateSlot.GetComponent<CreateNewSlots>();

        Inicialization();

    }

    public void Inicialization()
    {
        while (i < InventoryPanel.childCount)
        {
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
            {
                InventorySlots slot = InventoryPanel.GetChild(i).GetComponent<InventorySlots>();
                Slots.Add(slot);
                if (slot.Item != null && !spellDictionary.ContainsKey(slot.Item.name))
                {
                    spellDictionary.Add(slot.Item.name, slot.Item);
                }
                i++;
            }
        }
    }


    int Temperature;
    int Force;
    int Amperage;
    int Gravity;
    int Light;

    public void LoadData(GameData data)
    {
        //int s = 2;
        //foreach (InventorySlots slot in Slots)
        //{
        //    if (!slot.isEmpty && slot.Item.name == null)
        //    {
        //        slot.Item.name = s.ToString();
        //    }
        //}
        int j = 0;

        foreach (string spellName in data.spellNames)
        {
            Debug.Log("Размер - " + data.spellAtributs.Count);

            Temperature = data.spellAtributs[j];
            j++;

            Force = data.spellAtributs[j];
            j++;

            Amperage = data.spellAtributs[j];
            j++;

            Gravity = data.spellAtributs[j];
            j++;

            Light = data.spellAtributs[j];
            j++;

            Debug.Log("Всё ок " + spellName);


            CreateSpell(Temperature, Force, Amperage, Gravity, Light);

        }


        if (data.fastSlots != null && data.fastSlots.Count > 0)
        {
            if (!data.fastSlotsEmpty[0])
            {
                CreateSpellFastSlots(data.fastSlots[0], data.fastSlots[1], data.fastSlots[2], data.fastSlots[3], data.fastSlots[4], FastSlot1);
            }
            if (!data.fastSlotsEmpty[1])
            {
                CreateSpellFastSlots(data.fastSlots[5], data.fastSlots[6], data.fastSlots[7], data.fastSlots[8], data.fastSlots[9], FastSlot2);
            }
            if (!data.fastSlotsEmpty[2])
            {
                CreateSpellFastSlots(data.fastSlots[10], data.fastSlots[11], data.fastSlots[12], data.fastSlots[13], data.fastSlots[14], FastSlot3);
            }
            if (!data.fastSlotsEmpty[3])
            {
                CreateSpellFastSlots(data.fastSlots[15], data.fastSlots[16], data.fastSlots[17], data.fastSlots[18], data.fastSlots[19], FastSlot4);
            }
        }


    }

    public void SaveData(GameData data)
    {
        foreach (InventorySlots slot in Slots)
        {
            if (slot.Item != null && !data.spellNames.Contains(slot.Item.name) && slot.Item.name != "1000000" && slot.Item.name != "1000001" && slot.Item.name != "" && slot.Item.name != " ")
            {
                data.spellNames.Add(slot.Item.name);

                Debug.Log("Я родился! - " + slot.Item.name);

                data.spellAtributs.Add(slot.Item.Temperature);
                data.spellAtributs.Add(slot.Item.Force);
                data.spellAtributs.Add(slot.Item.Amperage);
                data.spellAtributs.Add(slot.Item.Gravity);
                data.spellAtributs.Add(slot.Item.Light);
            }
        }

        //Debug.Log(FastSlot1.GetComponent<InventorySlots>().Item.Temperature);
        if (!FastSlot1.GetComponent<InventorySlots>().isEmpty)
        {
            data.fastSlots.Add(FastSlot1.GetComponent<InventorySlots>().Item.Temperature);
            data.fastSlots.Add(FastSlot1.GetComponent<InventorySlots>().Item.Force);
            data.fastSlots.Add(FastSlot1.GetComponent<InventorySlots>().Item.Amperage);
            data.fastSlots.Add(FastSlot1.GetComponent<InventorySlots>().Item.Gravity);
            data.fastSlots.Add(FastSlot1.GetComponent<InventorySlots>().Item.Light);
        }
        else
        {
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
        }

        if (!FastSlot2.GetComponent<InventorySlots>().isEmpty)
        {
            data.fastSlots.Add(FastSlot2.GetComponent<InventorySlots>().Item.Temperature);
            data.fastSlots.Add(FastSlot2.GetComponent<InventorySlots>().Item.Force);
            data.fastSlots.Add(FastSlot2.GetComponent<InventorySlots>().Item.Amperage);
            data.fastSlots.Add(FastSlot2.GetComponent<InventorySlots>().Item.Gravity);
            data.fastSlots.Add(FastSlot2.GetComponent<InventorySlots>().Item.Light);
        }
        else
        {
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
        }


        if (!FastSlot3.GetComponent<InventorySlots>().isEmpty)
        {
            data.fastSlots.Add(FastSlot3.GetComponent<InventorySlots>().Item.Temperature);
            data.fastSlots.Add(FastSlot3.GetComponent<InventorySlots>().Item.Force);
            data.fastSlots.Add(FastSlot3.GetComponent<InventorySlots>().Item.Amperage);
            data.fastSlots.Add(FastSlot3.GetComponent<InventorySlots>().Item.Gravity);
            data.fastSlots.Add(FastSlot3.GetComponent<InventorySlots>().Item.Light);
        }
        else
        {
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
        }

        if (!FastSlot4.GetComponent<InventorySlots>().isEmpty)
        {
            data.fastSlots.Add(FastSlot4.GetComponent<InventorySlots>().Item.Temperature);
            data.fastSlots.Add(FastSlot4.GetComponent<InventorySlots>().Item.Force);
            data.fastSlots.Add(FastSlot4.GetComponent<InventorySlots>().Item.Amperage);
            data.fastSlots.Add(FastSlot4.GetComponent<InventorySlots>().Item.Gravity);
            data.fastSlots.Add(FastSlot4.GetComponent<InventorySlots>().Item.Light);
        }
        else
        {
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
            data.fastSlots.Add(0);
        }


        data.fastSlotsEmpty.Add(FastSlot1.GetComponent<InventorySlots>().isEmpty);
        data.fastSlotsEmpty.Add(FastSlot2.GetComponent<InventorySlots>().isEmpty);
        data.fastSlotsEmpty.Add(FastSlot3.GetComponent<InventorySlots>().isEmpty);
        data.fastSlotsEmpty.Add(FastSlot4.GetComponent<InventorySlots>().isEmpty);
    }


    public int CountSpell = 0;

    public void CreateSpell(int Temperature, int Force, int Amperage, int Gravity, int light)
    {
        SpellItem spellItem = ScriptableObject.CreateInstance<SpellItem>();

        // Настройка свойств SpellItem
        spellItem.Temperature = Temperature;
        spellItem.Force = Force;
        spellItem.Amperage = Amperage;
        spellItem.Gravity = Gravity;
        spellItem.Light = light;

        CountSpell++;

        spellItem.Icon = SpellArt;
        spellItem.ItemName = CountSpell - 2 + UnityEngine.Random.Range(100, 997);



        //string targetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items" + "/" + CountSpell + ".asset";

        //#if UNITY_EDITOR
        // AssetDatabase.CreateAsset(spellItem, targetPath);
        // AssetDatabase.SaveAssets();
        //#endif

        Inicialization();

        Instantiate(prefab, InventoryPanel); // Создаем префаб как дочерний элемент панели Content
        AddItem(spellItem);

    }


    public void CreateSpellFastSlots(int Temperature, int Force, int Amperage, int Gravity, int light, GameObject FS)
    {
        SpellItem spellItem = ScriptableObject.CreateInstance<SpellItem>();

        // Настройка свойств SpellItem
        spellItem.Temperature = Temperature;
        spellItem.Force = Force;
        spellItem.Amperage = Amperage;
        spellItem.Gravity = Gravity;
        spellItem.Light = light;

        CountSpell++;

        spellItem.Icon = SpellArt;
        spellItem.ItemName = CountSpell;


        //string targetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items" + "/" + CountSpell + ".asset";

        //#if UNITY_EDITOR
        // AssetDatabase.CreateAsset(spellItem, targetPath);
        // AssetDatabase.SaveAssets();
        //#endif

        Inicialization();

        Instantiate(prefab, InventoryPanel); // Создаем префаб как дочерний элемент панели Content
        AddItemFastSlots(spellItem, FS);

    }



    public void AddItem(ItemScriptableObject _item)
    {
        //Instantiate(prefab, InventoryPanel);
        Inicialization();

        foreach (InventorySlots slot in Slots)
        {
            if (slot.isEmpty)
            {
                slot.Item = _item;
                slot.isEmpty = false;
                slot.isHighlighting = false;
                slot.Item.name = CountSpell.ToString();

                slot.SetIcon(_item.Icon);
                slot.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
                break;
            }
        }
    }

    public void AddItemFastSlots(ItemScriptableObject _item, GameObject FS)
    {
        //Instantiate(prefab, InventoryPanel);
        Inicialization();
        if (FS.GetComponent<InventorySlots>().isEmpty)
        {
            FS.GetComponent<InventorySlots>().Item = _item;
            FS.GetComponent<InventorySlots>().isEmpty = false;
            FS.GetComponent<InventorySlots>().isHighlighting = false;

            FS.GetComponent<InventorySlots>().SetIcon(_item.Icon);
            FS.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
        }
    }

}
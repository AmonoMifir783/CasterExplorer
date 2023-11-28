using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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

    public GameObject prefab; // ������ �� ������, ������� ����� �������
    //public Transform contentPanel;


    public GameObject CreateSlot;

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
    }

    public void SaveData(GameData data)
    {
        foreach (InventorySlots slot in Slots)
        {
            if (slot.Item != null && !data.spellNames.Contains(slot.Item.name) && slot.Item.name != "1" && slot.Item.name != "2")
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

        spellItem.Icon = GenerateTexture();
        spellItem.ItemName = CountSpell;


            string targetPath = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Items" + "/" + CountSpell + ".asset";

            #if UNITY_EDITOR
                AssetDatabase.CreateAsset(spellItem, targetPath);
                AssetDatabase.SaveAssets();
            #endif

            Inicialization();

            Instantiate(prefab, InventoryPanel); // Создаем префаб как дочерний элемент панели Content
            AddItem(spellItem);

    }      

    public void AddItem(ItemScriptableObject _item)
    {
        Instantiate(prefab, InventoryPanel); // ������� ������ ��� �������� ������� ������ Content
        Inicialization();
        //Instantiate(prefab, contentPanel); // ������� ������ ��� �������� ������� ������ Content
        foreach (InventorySlots slot in Slots)
        {
            if (slot.isEmpty)
            {
                slot.Item = _item;
                slot.isEmpty = false;
                slot.isHighlighting = false;
                slot.SetIcon(_item.Icon);
                break;
            }
        }
    }



    public int width = 256;
    public int height = 256;
    public Texture2D texture;



    public Sprite GenerateTexture()
    {
        // ������� ����� �������� � ��������� ���������
        texture = new Texture2D(width, height);

        // ��������� ��� ������� �������� ��������� ������
        Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        Color32[] pixels = new Color32[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        // ��������� �������� ���������
        texture.SetPixels32(pixels);

        // ��������� ��������� � ��������
        texture.Apply();

        // ������� ������ �� ��������
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        sprite.name = "name";
        //string targetPathh = "Assets/CasterExplorer/UI/SpellSystem/InventorySystem/Images" + "/" + sprite.name + ".png";
        //byte[] bytes = texture.EncodeToPNG();
        //System.IO.File.WriteAllBytes(targetPathh, bytes);

        //#if UNITY_EDITOR
        //        AssetDatabase.CreateAsset(sprite, targetPathh);
        //            AssetDatabase.SaveAssets();
        //        #endif
        return sprite;
    }



    
}